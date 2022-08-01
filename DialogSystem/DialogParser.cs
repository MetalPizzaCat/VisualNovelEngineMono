

using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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

    private List<Speaker> _speakers = new List<Speaker>();
    //the result dialog
    private Dialog _dialog;

    /// <summary>
    /// Object that handles line by line reading.
    /// Keeping this as a local reference to avoid having to pass it around in arguments
    /// </summary>
    private StringReader _dialogFileReader;

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
                    break;
                case "source":
                    // here we will have to reference a file that contains actual speaker info
                    Console.WriteLine($"Speaker source is {blocks[1]}");
                    break;
                case "position":
                    Console.WriteLine($"Speaker positions is {blocks[1]}");
                    break;
                default:
                    throw new Exception($"Invalid property found when parsing speaker info. Property: {blocks[0]}");
            }
            line = _getNextProperLine();
        }
    }

    private void _parseTextDialog()
    {
        string? line = _getNextProperLine();
        string label = string.Empty;
        //TODO: this is weird 
        Match match = _blockNameRegEx.Match((line ?? throw new NullReferenceException("Dialog is missing a name label")));
        if (match.Success)
        {
            label = match.Value;
        }

        List<DialogSystem.DialogActionBase> actions = new List<DialogActionBase>();
        line = _getNextProperLine();
        while (line != null || line == string.Empty)
        {
            //actions don't contain ":" symbol
            if (line.Contains(":"))
            {
                int pos = line.IndexOf(":");
                string name = line.Substring(0, pos);
                int speakerId = _speakers.FindIndex(0, p => p.DisplayName == name);
                //process as text
                actions.Add(new DialogSystem.TextAction(line.Substring(pos), speakerId));
            }
            else
            {
                line.ToLower();
                if (line.StartsWith("jump"))
                {
                    string[] bits = line.Split(" ");
                    if (bits.Length < 2)
                    {
                        throw new Exception("Invalid amount of info passed to the jump command. It should be 'jump location'");
                    }
                    actions.Add(new JumpAction(bits[1]));
                }
                else if (line.StartsWith("exit"))
                {
                    actions.Add(new ExitAction());
                }
                else if (line.StartsWith("speaker"))
                {
                    //TODO: implement not implemented implementation
                    throw new NotImplementedException();
                }
            }
            line = _getNextProperLine();
        }
        _dialog.DialogItems.Add(label, new DialogSystem.BlockData(BlockType.Text, actions));
    }

    private void _parseDialogOption()
    {
        throw new NotImplementedException();
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
                Console.WriteLine("This os options block");
                _parseDialogOption();
                break;
            case "[speaker]":
                Console.WriteLine("This is speaker block");
                _parseSpeaker();
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