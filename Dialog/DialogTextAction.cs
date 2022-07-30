using System.Collections.Generic;

using Microsoft.Xna.Framework;

using UI;

public class DialogTextAction : DialogActionBase
{
    /// <summary>
    /// Defines what happens once dialog is finished
    /// </summary>
    public readonly DialogEventType EndEventType = DialogEventType.Exit;
    public List<DialogText> Text { get; set; } = new List<DialogText>();
    private Label _textLabel;

    private int _currentTextId = 0;

    public DialogTextAction(Dialog dialog) : base(dialog)
    {
        _textLabel = new Label(dialog.Game, "DEFAULT TEXT", Vector2.Zero);
    }

    public override void Init()
    {
        base.Init();
        Dialog.Game.AddUiElement(_textLabel);
        _textLabel.OnClicked += _onLabelClicked;
    }

    private void _onLabelClicked()
    {
        //change text
        if (++_currentTextId < Text.Count)
        {
            _textLabel.Text = Text[_currentTextId].Text;
        }
        else
        {
            //TODO: Jump
        }
    }

    public override void Clear()
    {
        base.Clear();
    }
}