using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace DialogSystem;

/// <summary>
/// Class that handles parsing dialog file into dialog objects
/// </summary>
public class DialogParser
{
    private string _filePath;

    //(?&lt;=\[) is (?<=\[)
    /// <summary>
    /// Regular Expression for checking if line is block type marker<br/>
    /// (?&lt;=\[) and (?=\]) use lookahead and lookbehind to make sure text is hidden in the brackets but avoid including the bracket<br/>
    /// ([a-z])\w+ find a word
    /// </summary>
    private readonly Regex _typeNameRegEx = new Regex(@"(?<=\[)([a-z])\w+(?=\])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private readonly Regex _blockNameRegEx = new Regex(@"(?<=\{)([a-z])\w+(?=\})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    /// <summary>
    /// Any text that is in quotes, but not capturing quotation marks
    /// </summary>
    private readonly Regex _stringLiteralVariableRegEx = new Regex("(?<=\").*?(?=\")", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    //the result dialog
    private Dialog _dialog;

    /// <summary>
    /// Object that handles line by line reading.
    /// Keeping this as a local reference to avoid having to pass it around in arguments
    /// </summary>
    private StringReader _dialogFileReader;

    private SpeakerData? _loadSpeakerData(string name)
    {
        string speakerInfo = File.ReadAllText($"./assets/{name.Trim()}.json");
        return JsonConvert.DeserializeObject<SpeakerData>(speakerInfo);
    }

    /// <summary>
    /// Gets next line from StringReader ignoring empty lines or comments.
    /// This also trims the line
    /// </summary>
    private string? _getNextProperLine()
    {
        string? result = null;
        while ((result = _dialogFileReader.ReadLine()) != null)
        {
            result.Trim();
            // comments are any lines that start with #
            // and empty lines are empty lines
            // duh
            if (result.StartsWith("#"))
            {
                continue;
            }
            return result;
        }
        return null;
    }

    private void _parseSpeaker()
    {
        /*
            Possible idea for improving parser
            Instead of just trying to read all properties and causing massive issues when we read further the allowed data
            Instead we can read until the empty line and throw an error if not all fields were filled

            For parsing properties we will just split it in two by '='
            because property is just "name= value", so name is split[0] and value is split[1]
            we can apply the same logic to the dialog text, but split using ':' where right is the speaker name and left 
            is speaker text
        */
        string? line = _getNextProperLine();
        string speakerName = string.Empty;
        SpeakerData? data = null;
        SpeakerPosition pos = SpeakerPosition.Offscreen;
        while (line != null && line != string.Empty)
        {
            string[] blocks = line.Split("=");
            if (blocks.Length != 2)
            {
                throw new Exception("Invalid property string. Property string should be 'name=value'");
            }

            switch (blocks[0].Trim())
            {
                case "name":
                    Console.WriteLine($"Speaker name is {blocks[1]}");
                    speakerName = blocks[1].Trim();
                    break;
                case "source":
                    // here we will have to reference a file that contains actual speaker info
                    Console.WriteLine($"Speaker source is {blocks[1]}");
                    data = _loadSpeakerData(blocks[1]);
                    break;
                case "position":
                    Console.WriteLine($"Speaker positions is {blocks[1]}");
                    Enum.TryParse<SpeakerPosition>(blocks[1].Trim(), true, out pos);
                    break;
                default:
                    throw new Exception($"Invalid property found when parsing speaker info. Property: {blocks[0]}");
            }

            line = _getNextProperLine();
        }
        _dialog.Speakers.Add(new Speaker(_dialog, _dialog.SpeakerSize, pos, data ?? throw new NullReferenceException("No speaker data found"), speakerName, _dialog.Game));
    }

    private DialogSystem.DialogActionBase? _parseActionLine(string line)
    {
        line.ToLower();
        if (line.StartsWith("jump"))
        {
            string[] bits = line.Split(" ");
            if (bits.Length < 2)
            {
                throw new Exception("Invalid amount of info passed to the jump command. It should be 'jump location'");
            }
            return new JumpAction(bits[1]);
        }
        else if (line.StartsWith("exit"))
        {
            return new ExitAction();
        }
        else if (line.StartsWith("speaker"))
        {
            //speaker [who] [action] [info]
            string[] blocks = line.Split(" ");
            switch (blocks[2])
            {
                case "move":
                    SpeakerPosition pos = SpeakerPosition.Offscreen;
                    Enum.TryParse<SpeakerPosition>(blocks[3], true, out pos);
                    return new SpeakerMoveAction(blocks[1].Trim(), pos);
                case "change":
                    return new SpeakerStateAction(blocks[1].Trim(), blocks[3]);
            }
        }
        return null;
    }

    /// <summary>
    /// This function goes over the actual data block of the text block<br/>
    /// Functions were split so that embedded blocks would be easier to parse
    /// </summary>
    /// <returns>List of actions that were in the blocks</returns>
    private List<DialogSystem.DialogActionBase> _parseTextDialogData()
    {
        List<DialogSystem.DialogActionBase> actions = new List<DialogActionBase>();
        string? line = _getNextProperLine();
        //blocks ends if there is End Of File, empty line or label that doesn't belong in the text block
        while (line != null && line != string.Empty && line != "end")
        {
            //actions don't contain ":" symbol
            if (line.Contains(":"))
            {
                int pos = line.IndexOf(":");
                string name = line.Substring(0, pos).Trim();
                int speakerId = _dialog.Speakers.FindIndex(0, p => p.Name == name);
                //process as text
                actions.Add(new DialogSystem.TextAction(line.Substring(pos + 1).Trim(), speakerId));
            }
            else
            {
                actions.Add(_parseActionLine(line) ?? throw new NullReferenceException("Failed to parse action line"));
            }
            line = _getNextProperLine();
        }
        return actions;
    }

    private void _parseTextDialog()
    {
        string? line = _getNextProperLine();
        string label = string.Empty;
        //TODO: this is weird 
        Match match = _blockNameRegEx.Match((line ?? throw new NullReferenceException("Dialog is missing a name label")));
        if (!match.Success)
        {
            throw new Exception("Dialog block is missing name label");
        }
        label = match.Value;
        _dialog.DialogItems.Add(label, new DialogSystem.BlockData(BlockType.Text, _parseTextDialogData()));
    }

    private void _parseDialogOption()
    {
        string? line = _getNextProperLine();
        string label = string.Empty;
        //TODO: this is weird 
        Match match = _blockNameRegEx.Match((line ?? throw new NullReferenceException("Dialog is missing a name label")));
        if (!match.Success)
        {
            throw new Exception("Dialog block is missing name label");
        }
        label = match.Value;
        //current option

        List<DialogSystem.DialogActionBase> actions = new List<DialogActionBase>();
        line = _getNextProperLine();
        /**
        The way option reading works is by reading option line itself and then parsing everything till end of option/block 
        as an "anonymous" text block
        This way dialog displaying logic doesn't need to be duplicated
        */
        int jumpIndex = 0;
        //read lines until have stuff to read
        while (line != null && line != string.Empty && line != "end")
        {
            //this is an option
            if (line.Contains(":"))
            {
                string[] tokens = line.Split(":");
                //count is last index + 1 and we want to jump to action that is right after this one
                DialogSystem.OptionAction option = new OptionAction(tokens[1], $"{label}_AUTO_GEN_{jumpIndex}");
                actions.Add(option);
                _dialog.DialogItems.Add($"{label}_AUTO_GEN_{jumpIndex++}", new DialogSystem.BlockData(BlockType.Text, _parseTextDialogData()));
            }
            line = _getNextProperLine();
        }
        _dialog.DialogItems.Add(label, new DialogSystem.BlockData(BlockType.Option, actions));
    }

    private void _processVariables()
    {
        string? line = _getNextProperLine();
        while (line != null && line != string.Empty && line != "end")
        {
            string[] tokens = line.Split("=");
            if (tokens.Length != 2)
            {
                throw new Exception("Invalid variable assignment line");
            }
            Match match = _stringLiteralVariableRegEx.Match(tokens[1]);
            if (match.Success)
            {
                _dialog.Variables.Add(tokens[0].Trim(), new DialogVariable(tokens[0].Trim(), match.Value));
            }
            else
            {
                _dialog.Variables.Add(tokens[0].Trim(), new DialogVariable(tokens[0].Trim(), float.Parse(tokens[1].Trim())));
            }
            line = _getNextProperLine();
        }
    }

    private void _processType(string? line)
    {
        if (line == null)
        {
            return;
        }
        switch (line)
        {
            case "[dialog]":
                Console.WriteLine("This is dialog block");
                _parseTextDialog();
                break;
            case "[options]":
                Console.WriteLine("This is options block");
                _parseDialogOption();
                break;
            case "[speaker]":
                Console.WriteLine("This is speaker block");
                _parseSpeaker();
                break;
            case "[variables]":
                Console.WriteLine("This is variables block");
                _processVariables();
                break;
        }
    }
    public DialogParser(string dialogPath)
    {
        _filePath = dialogPath;
    }

    public Dialog ParseDialog(VisualNovelMono.VisualNovelGame game)
    {
        string dialogFile = File.ReadAllText(_filePath);
        _dialog = new Dialog(game);
        using (_dialogFileReader = new StringReader(dialogFile))
        {
            string? line = string.Empty;
            while ((line = _getNextProperLine()) != null)
            {
                if (_typeNameRegEx.IsMatch(line))
                {
                    _processType(line);
                    continue;
                }
            }
            Console.WriteLine("Finished parsing");
        }
        return _dialog;
    }
}