using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Scene : GameObject
{
    public Texture2D? BackgroundTexture { get; set; }
    public Point ScreenSize { get; set; }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        spriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, ScreenSize.X, ScreenSize.Y), null, Color.White);
    }
}