using Microsoft.Xna.Framework;

namespace DialogSystem;

public class TextAction : DialogSystem.DialogActionBase
{
    public string Text { get; set; }
    public int Speaker { get; set; }
    public TextAction(string text, int speaker)
    {
        _type = DialogActionType.Text;
        Text = text;
        Speaker = speaker;
    }
}