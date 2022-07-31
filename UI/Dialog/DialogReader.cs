using UI;
using Microsoft.Xna.Framework;

/// <summary>
/// This element 
/// </summary>
public class DialogReader : UserInterfaceElement
{
    public delegate void DialogEventHandler(DialogEventType type);

    /// <summary>
    /// Called when dialog hits event such as jump or exit
    /// </summary>
    public event DialogEventHandler DialogEvent;
    private DialogTextAction? _dialogText;
    private int _currentLine = 0;
    private Label _label;

    public DialogTextAction? DialogTextAction
    {
        get => _dialogText;
        set
        {
            _dialogText = value;
            _currentLine = 0;
            _displayNextLine(this);
        }
    }

    public DialogReader(Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(position, size, game)
    {
        _label = new Label(game, "DEFAULT_TEXT_OONGA_BOONGA", position);
        game.AddUiElement(_label);
        _label.OnClicked += _displayNextLine;
    }

    private void _displayNextLine(UserInterfaceElement sender)
    {
        if (_dialogText != null)
        {
            _label.Text = _dialogText.Text[_currentLine++].Text;
            if (_currentLine >= _dialogText.Text.Count)
            {
                DialogEvent?.Invoke(_dialogText.EndEventType);
            }
        }
    }
}