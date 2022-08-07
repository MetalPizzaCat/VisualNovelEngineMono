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
    private DialogSystem.DialogOptionBlock _optionBlock;
    /// <summary>
    /// Key value pair where key is the dialog label and value 
    /// is the action that will happen when dialog jumps to that label
    /// </summary>
    public Dictionary<string, BlockData> DialogItems { get; set; } = new Dictionary<string, BlockData>();

    /// <summary>
    /// Everyone who speaks
    /// </summary>
    public List<Speaker> Speakers { get; set; } = new List<Speaker>();

    public Vector2 SpeakerSize { get; set; } = new Vector2(400, 400);
    public Vector2 SceneSize { get; set; } = Vector2.One;

    public Vector2 CenterPosition => new Vector2(SceneSize.X / 2, 0);
    public Vector2 RightPosition => new Vector2(SceneSize.X - SpeakerSize.X, 0);
    public Vector2 LeftPosition => new Vector2(0, 0);

    public Vector2 OffscreenPosition => new Vector2(SceneSize.X + 1, 0);

    private string? _currentLabel;
    public string? CurrentLabel => _currentLabel;

    public void AdvanceDialog(string? newLabel)
    {
        _currentLabel = newLabel ?? DialogItems.Keys.First();
        switch (DialogItems[_currentLabel].DataType)
        {
            case BlockType.Text:
                _textBlock.ChangeActions(DialogItems[_currentLabel].Actions);
                _optionBlock.Visible = false;
                _textBlock.Visible = true;
                break;
            case BlockType.Option:
                _optionBlock.ChangeActions(DialogItems[_currentLabel].Actions);
                _optionBlock.Visible = true;
                _textBlock.Visible = false;
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

        _optionBlock = new DialogOptionBlock(new Vector2(300, 100), new Vector2(500, 500), game);
        _optionBlock.OnActionEvent += _onDialogEvent;
        game.AddUiElement(_optionBlock);
    }

    public virtual void Init()
    {
        foreach (Speaker speaker in Speakers)
        {
            switch (speaker.ScenePosition)
            {
                case SpeakerPosition.Center:
                    speaker.Position = CenterPosition;
                    break;
                case SpeakerPosition.Left:
                    speaker.Position = LeftPosition;
                    break;
                case SpeakerPosition.Right:
                    speaker.Position = RightPosition;
                    break;
                case SpeakerPosition.Offscreen:
                    speaker.Position = OffscreenPosition;
                    break;
            }
            Game.AddUiElement(speaker);
        }
    }
}