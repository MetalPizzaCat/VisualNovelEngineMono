using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.Xna.Framework;

using UI;
namespace DialogSystem;

/// <summary>
/// Simple block that advances text each time player presses.<br/>
/// If anything except text is placed in the list it will be executed
/// </summary>
public class DialogTextBlock : DialogSystem.DialogBlockBase
{
    private readonly Regex _variableRegEx = new Regex(@"\$.*?\$");
    private Label _label;
    private Label _speakerNameLabel;
    private int _currentLine = 0;
    public DialogTextBlock(Dialog dialog, Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(dialog, position, size, game)
    {
        _label = new Label(game, "", Position + new Vector2(32, 64));
        _label.OnClicked += _onLabelClicked;
        _label.Layer = RenderLayer.InteractionUI;
        AddChild(_label);
        game.AddUiElement(_label);

        _speakerNameLabel = new Label(game, "", Position);
        _label.Layer = RenderLayer.InteractionUI;
        AddChild(_speakerNameLabel);
        game.AddUiElement(_speakerNameLabel);
    }
    private void _changeLine(int lineId)
    {
        _currentLine = lineId;
        switch (Actions[_currentLine].Action)
        {
            //we only care about text action, so everything else is passed to dialog
            case DialogActionType.Text:
                if (Actions[_currentLine] is DialogSystem.TextAction text)
                {
                    _label.Text = Regex.Replace(text.Text, @"\$.*?\$",
                        match => Dialog.Variables[match.Value.Substring(1, match.Value.Length - 2)]?.ToString() ?? "%MISSING VARIABLE%");
                    _speakerNameLabel.Text = text.Speaker >= 0 ?
                        Dialog.Speakers[text.Speaker].SpeakerData.DisplayName :
                        string.Empty;
                }
                break;
            default:
                RaiseActionEvent(Actions[_currentLine]);
                break;
        }
    }

    private void _onLabelClicked(UserInterfaceElement elem)
    {
        if (Game.CurrentState != GameState.Normal)
            return;
        if (++_currentLine < Actions.Count)
        {
            _changeLine(_currentLine);
        }
        else
        {
            throw new System.Exception("Unexpected end of dialog reached");
        }
    }
    public override void ChangeActions(List<DialogActionBase> actions)
    {
        base.ChangeActions(actions);
        _changeLine(0);
    }
}