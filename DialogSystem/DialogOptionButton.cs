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
    ) : base(game,option.Text, position, size, new Rectangle(0, 0, 32, 32))
    {
        Option = option;
    }
}