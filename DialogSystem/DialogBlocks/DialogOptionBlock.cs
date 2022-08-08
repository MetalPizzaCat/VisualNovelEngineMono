using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Linq;

using UI;
namespace DialogSystem;

/// <summary>
/// Option block provides user with ability to select an option<br/>
/// Once option is selected game will jump to an anonoymous text block, which will execute rest of the actions
/// </summary>
public class DialogOptionBlock : DialogBlockBase
{
    private List<DialogOptionButton> _options = new List<DialogOptionButton>();
    private int _currentLine = 0;
    public DialogOptionBlock(Dialog dialog, Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(dialog, position, size, game)
    {

    }

    private void _onButtonClicked(UserInterfaceElement sender)
    {
        if (sender is DialogOptionButton button)
        {
            RaiseActionEvent(new JumpAction(button.Option.JumpLocation));
        }
    }

    public override void ChangeActions(List<DialogActionBase> actions)
    {
        base.ChangeActions(actions);
        foreach (GameObject btn in _options)
        {
            btn.Destroy();
        }
        _options.Clear();

        IEnumerable<DialogActionBase> options = actions.Where(p => p is OptionAction);
        int i = 0;
        foreach (DialogActionBase option in options)
        {
            DialogOptionButton btn = new DialogOptionButton(option as OptionAction, Game, new Vector2(0, i++ * 64) + Position, new Vector2(500, 64));
            _options.Add(btn);
            Game.AddUiElement(btn);
            AddChild(btn);
            btn.OnClicked += _onButtonClicked;
        }
        _currentLine = 0;
    }
}