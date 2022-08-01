namespace DialogSystem;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

/// <summary>
/// Represents the whole dialog tree
/// </summary>
public class Dialog
{
    public VisualNovelMono.VisualNovelGame Game;
    private DialogSystem.DialogTextBlock _textBlock;
    /// <summary>
    /// Key value pair where key is the dialog label and value 
    /// is the action that will happen when dialog jumps to that label
    /// </summary>
    public Dictionary<string, List<DialogSystem.DialogActionBase>> DialogItems { get; set; } = new Dictionary<string, List<DialogSystem.DialogActionBase>>();

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
        if (item is DialogSystem.DialogTextBlock dialogText)
        {
            _textBlock.ChangeActions()
        }
        else if (item is DialogOptionAction option)
        {
            _displayDialogOptions(option);
        }
    }

    private void _showDialogText(DialogSystem.DialogTextBlock block)
    {
        _textBlock.
    }

    private void _onDialogEvent(DialogEventType type)
    {
        switch (type)
        {
            case DialogEventType.Jump:
                System.Console.WriteLine("Jumping!");
                //TODO: remove this temp solution
                AdvanceDialog("{choice1}");
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
        _options = new DialogOptions(Vector2.Zero, Vector2.Zero, game);
        _options.DialogEvent += _onDialogEvent;

        game.AddUiElement(_reader);
        game.AddUiElement(_options);
    }
}