using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Linq;

using UI;
namespace DialogSystem;

/**
example of the option block
option1
action 
action
option2
action
option3
action
default:none
action

if option1 is selected block will perform every action until option2 is hit
if no jump or exit action is provided
dialog will then jump to default:none block and execute every action located there
*/
/// <summary>
/// Option block provides user with ability to select an option<br/>
/// Once option is selected it will perform every action(until next option is hit)
/// </summary>
public class DialogOptionBlock : DialogBlockBase
{
    private List<DialogOptionButton> _options = new List<DialogOptionButton>();
    private int _currentLine = 0;
    public DialogOptionBlock(Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(position, size, game)
    {

    }

    private void _executeActions()
    {
        while (Actions[_currentLine].Action != DialogActionType.Option)
        {
            RaiseActionEvent(Actions[_currentLine++]);
        }
    }

    private void _onButtonClicked(UserInterfaceElement sender)
    {
        if (sender is DialogOptionButton button)
        {
            _currentLine = button.Option.JumpLocation;
            _executeActions();
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
            DialogOptionButton btn = new DialogOptionButton(option as OptionAction, Game, new Vector2(0, i++ * 64) + Position, new Vector2(64, 64));
            _options.Add(btn);
            Game.AddUiElement(btn);
            AddChild(btn);
            btn.OnClicked += _onButtonClicked;
        }
        _currentLine = 0;
    }
}