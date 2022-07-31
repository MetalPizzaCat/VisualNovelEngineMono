using Microsoft.Xna.Framework;

using System.Collections.Generic;

using UI;
public class DialogOptions : UI.UserInterfaceElement
{
    public delegate void DialogEventHandler(DialogEventType type);

    /// <summary>
    /// Called when dialog hits event such as jump or exit
    /// </summary>
    public event DialogEventHandler DialogEvent;

    private List<Button> _buttons = new List<Button>();
    private DialogOptionAction _action;

    public override bool Visible
    {
        get => base.Visible; set
        {
            base.Visible = value;
            foreach (Button btn in _buttons)
            {
                btn.Visible = value;
            }
        }
    }

    public DialogOptionAction Action
    {
        get => _action;
        set
        {
            _action = value;
            int i = 0;
            foreach (DialogOption option in _action.Options)
            {
                Button btn = new Button(Game, new Vector2(0, i * 50), new Vector2(64, 64));
                _buttons.Add(btn);
                btn.OnClicked += _onOptionClicked;
                Game.AddUiElement(btn);
            }
        }
    }

    public DialogOptions(Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(position, size, game)
    {

    }

    private void _onOptionClicked(UserInterfaceElement button)
    {
        
    }
}