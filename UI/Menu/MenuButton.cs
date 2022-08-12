using Microsoft.Xna.Framework;

using VisualNovelMono;

namespace UI;

public class MenuButton : Button
{
    public MenuButton(VisualNovelGame game, string text, Vector2 position, Vector2 size, Rectangle? srcRect = null) : base(game, text, position, size, srcRect)
    {
        
    }
}