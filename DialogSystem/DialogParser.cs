

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
    /// <summary>
    /// Regular Expression for checking if line is block type marker
    /// </summary>
    private readonly Regex _typeNameRegEx = new Regex(@"\[([a-z])\w+\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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
    private DialogText _parseDialogLine(string line)
    {
        //all dialog follows 'name:text' standard
        //if dialog has no specific speaker '0:' is used
        //which is why we split based on first occurrence of ":" instead of anything else
        int separatorIndex = line.IndexOf(":");
        if (separatorIndex == -1)
        {
            throw new Exception("Invalid dialog line passed. All lines must follow 'name:text' standard");
        }
        string text = line.Substring(separatorIndex);
        string speaker = line.Substring(0, separatorIndex);

        return new DialogText(text, null);
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
        string label = line ?? throw new NullReferenceException("Dialog is missing a name label");
        DialogTextAction action = new DialogTextAction(_dialog);
        while ((line = _getNextProperLine()) != null)
        {
            if (line == string.Empty)
            {
                throw new Exception("Dialog is missing 'exit' or 'jump' label");
            }
            //TODO: replace this with text-to-action-label converter
            if (line.StartsWith("exit"))
            {
                //mark final action as game exit
                break;
            }
            else if (line.StartsWith("jump"))
            {
                //mark final action as dialog jump
                break;
            }

            action.Text.Add(_parseDialogLine(line));
        }
        _dialog.DialogItems.Add(label, action);
    }

    private void _parseDialogOption()
    {
        /** Example
        [options]
        {choice1}
        girl: No u
        jump petty
        girl: Sure i do, what's your point
        jump die
        girl: Basinga
        jump dialog*/
        string? line = _getNextProperLine();
        string label = line ?? throw new NullReferenceException("Dialog is missing a name label");
        DialogOptionAction action = new DialogOptionAction(_dialog);
        line = _getNextProperLine();
        while (line != null && line != string.Empty)
        {
            DialogText text = _parseDialogLine(line);
            line = _getNextProperLine();
            DialogEventType type = DialogEventType.Exit;
            string[] actionInfo = line?.Split(" ") ?? throw new NullReferenceException("No dialog text provided");
            switch (actionInfo[0])
            {
                case "exit":
                    type = DialogEventType.Exit;
                    break;
                case "jump":
                    type = DialogEventType.Jump;
                    break;
            }
            //TODO: Fix dialog option not taking right text type
            action.Options.Add(new DialogOption(type, text.Text, actionInfo[1]));
            line = _getNextProperLine();
        }
        _dialog.DialogItems.Add(label, action);
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