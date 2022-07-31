using Microsoft.Xna.Framework;

using System.Collections.Generic;

using UI;
public class DialogOptions : DialogUIElement
{

    private List<DialogOptionButton> _buttons = new List<DialogOptionButton>();
    private DialogOptionAction _action;

    public override bool Visible
    {
        get => base.Visible; set
        {
            base.Visible = value;
            foreach (DialogOptionButton btn in _buttons)
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
                DialogOptionButton btn = new DialogOptionButton(option.Text, option, Game, new Vector2(0, i++ * 50), new Vector2(64, 64), new Rectangle(0, 0, 32, 32));
                _buttons.Add(btn);
                btn.OnOptionSelected += _onOptionSelected;
                Game.AddUiElement(btn, true);
            }
        }
    }

    public DialogOptions(Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(position, size, game) { }

    private void _onOptionSelected(DialogOption option)
    {
        RaiseDialogEvent(option.EventType);
        System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(option));
    }
}