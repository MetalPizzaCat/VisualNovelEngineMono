using System.Collections.Generic;

using Microsoft.Xna.Framework;

using UI;
namespace DialogSystem;

/// <summary>
/// Simple block that advances text each time player presses.<br/>
/// If anything except text is placed in the list it will be executed
/// </summary>
public class DialogTextBlock : DialogSystem.DialogBlockBase
{
    private Label _label;
    private int _currentLine = 0;
    public DialogTextBlock(Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(position, size, game)
    {
        _label = new Label(game, "", Position + new Vector2(32, 32));
        _label.OnClicked += _onLabelClicked;
        _label.Layer = RenderLayer.InteractionUI;
        AddChild(_label);
        game.AddUiElement(_label);
    }
    private void _changeLine(int lineId)
    {
        _currentLine = lineId;
        switch (Actions[_currentLine].Action)
        {
            //we only care about text action, so everything else is passed to dialog
            case DialogActionType.Text:
                _label.Text =
                    (
                        (Actions[_currentLine] as DialogSystem.TextAction)
                        ?? throw new System.Exception("Invalid action passed under TextAction label")
                    ).Text;
                break;
            default:
                RaiseActionEvent(Actions[_currentLine]);
                break;
        }
    }

    private void _onLabelClicked(UserInterfaceElement elem)
    {
        if (++_currentLine < Actions.Count)
        {
            _changeLine(_currentLine);
        }
        else
        {
            throw new System.Exception("Unexcepted end of dialog reached");
        }
    }
    public override void ChangeActions(List<DialogActionBase> actions)
    {
        base.ChangeActions(actions);
        _changeLine(0);
    }
}