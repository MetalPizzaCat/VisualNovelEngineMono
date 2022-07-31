using UI;
using Microsoft.Xna.Framework;

public class DialogUIElement : UserInterfaceElement
{
    public delegate void DialogEventHandler(DialogEventType type);

    /// <summary>
    /// Called when dialog hits event such as jump or exit
    /// </summary>
    public event DialogEventHandler DialogEvent;


    public DialogUIElement(Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(position, size, game)
    {

    }

    protected void RaiseDialogEvent(DialogEventType type)
    {
        DialogEvent?.Invoke(type);
    }
}