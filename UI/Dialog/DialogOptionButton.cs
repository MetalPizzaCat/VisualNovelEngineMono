using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using UI;

/// <summary>
/// Button with text that contains info about dialog option
/// </summary>
public class DialogOptionButton : Button
{
    public delegate void OptionSelectedHandler(DialogOption option);

    public event OptionSelectedHandler? OnOptionSelected;
    private DialogOption _option;
    public DialogOptionButton
    (
        string text, DialogOption option, VisualNovelMono.VisualNovelGame game, Vector2 position, Vector2 size, Rectangle? srcRect = null
    ) : base(game, position, size, srcRect)
    {
        _option = option;
        Text = text;
    }

    public override void Draw(SpriteBatch batch)
    {
        base.Draw(batch);
        if (Visible)
        {
            batch.Draw(ButtonTextureAtlas, new Rectangle(Position.ToPoint(), BoundingBoxSize.ToPoint()), TextureSourceRectangle, Color.White);
            batch.DrawString(Font, Text ?? "no text lol", Position, Color.White);
        }
    }

    public override void Click()
    {
        base.Click();
        OnOptionSelected?.Invoke(_option);
    }
}