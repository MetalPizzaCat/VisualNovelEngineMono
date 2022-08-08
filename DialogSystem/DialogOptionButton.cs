using Microsoft.Xna.Framework;

using UI;

namespace DialogSystem;

public class DialogOptionButton : Button
{
    public readonly OptionAction Option;
    public DialogOptionButton
    (
        OptionAction option,
        VisualNovelMono.VisualNovelGame game,
        Vector2 position,
        Vector2 size,
        Rectangle? srcRect = null
    ) : base(game, option.Text, position, size, new Rectangle(0, 0, 16, 16))
    {
        Option = option;
    }

    public override void EnterMouse()
    {
        base.EnterMouse();

        TextureSourceRectangle = new Rectangle(16, 0, 16, 16);
    }

    public override void LeaveMouse()
    {
        base.LeaveMouse();

        TextureSourceRectangle = new Rectangle(0, 0, 16, 16);
    }
}