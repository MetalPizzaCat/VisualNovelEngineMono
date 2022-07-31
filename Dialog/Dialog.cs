using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

/// <summary>
/// Represents the whole dialog tree
/// </summary>
public class Dialog
{
    public VisualNovelMono.VisualNovelGame Game;
    /// <summary>
    /// Ui element used to display continuous lines of dialog
    /// </summary>
    private DialogReader _reader;

    /// <summary>
    /// Key value pair where key is the dialog label and value 
    /// is the action that will happen when dialog jumps to that label
    /// </summary>
    public Dictionary<string, DialogActionBase> DialogItems { get; set; } = new Dictionary<string, DialogActionBase>();

    /// <summary>
    /// Everyone who speaks.
    /// </summary>
    public List<Speaker> Speakers { get; set; } = new List<Speaker>();

    private string? _currentLabel;
    public string? CurrentLabel => _currentLabel;

    public void AdvanceDialog(string? newLabel)
    {
        _currentLabel = newLabel ?? DialogItems.Keys.First();
        DialogActionBase item = DialogItems[_currentLabel];
        //not the best way of iterating 
        if (item is DialogTextAction dialogText)
        {
            _displayDialogLines(dialogText);
        }
        else if (item is DialogOptionAction option)
        {
            _displayDialogOptions(option);
        }
    }

    private void _displayDialogLines(DialogTextAction action)
    {
        _reader.DialogTextAction = action;
    }

    private void _displayDialogOptions(DialogOptionAction action)
    {

    }

    private void _onDialogEvent(DialogEventType type)
    {
        switch (type)
        {
            case DialogEventType.Jump:
                System.Console.WriteLine("Jumping!");
                break;
            case DialogEventType.Exit:
                System.Console.WriteLine("Exit!");
                break;
        }
    }

    public Dialog(VisualNovelMono.VisualNovelGame game)
    {
        Game = game;
        _reader = new DialogReader(Vector2.Zero, Vector2.Zero, game);
        _reader.DialogEvent += _onDialogEvent;
    }
}