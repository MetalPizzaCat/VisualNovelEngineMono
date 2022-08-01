using Microsoft.Xna.Framework;

namespace DialogSystem;

public class TextAction : DialogSystem.DialogActionBase
{
    public string Text { get; set; }
    public TextAction(string text)
    {
        _type = DialogActionType.Text;
        Text = text;
    }
}