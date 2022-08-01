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
    public Dictionary<string, BlockData> DialogItems { get; set; } = new Dictionary<string, BlockData>();

    /// <summary>
    /// Everyone who speaks.
    /// </summary>
    public List<Speaker> Speakers { get; set; } = new List<Speaker>();

    private string? _currentLabel;
    public string? CurrentLabel => _currentLabel;

    public void AdvanceDialog(string? newLabel)
    {
        _currentLabel = newLabel ?? DialogItems.Keys.First();
        switch (DialogItems[_currentLabel].DataType)
        {
            case BlockType.Text:
                _textBlock.ChangeActions(DialogItems[_currentLabel].Actions);
                break;
            case BlockType.Option:
                throw new System.NotImplementedException("Option dialog is not implemented");
                break;
            default:
                throw new System.Exception("Invalid dialog block reached");
        }
    }

    private void _onDialogEvent(DialogActionBase action)
    {
        switch (action.Action)
        {
            case DialogActionType.Jump:
                if (action is JumpAction jump)
                {
                    AdvanceDialog(jump.Destination);
                    System.Console.WriteLine($"Jumping to {jump.Destination}");
                }
                break;
            case DialogActionType.Exit:
                throw new System.Exception("Exiting normally, this is not exception :D");
                break;
            case DialogActionType.Speaker:
                break;
        }
    }

    public Dialog(VisualNovelMono.VisualNovelGame game)
    {
        Game = game;
        _textBlock = new DialogTextBlock(new Vector2(300, 100), new Vector2(500, 500), game);
        _textBlock.OnActionEvent += _onDialogEvent;
        game.AddUiElement(_textBlock);
    }
}