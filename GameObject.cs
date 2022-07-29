using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Base for all scene objects that can be drawn or interacted with
/// </summary>
public class GameObject
{
    public virtual void LoadContent(ContentManager content) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void Init() { }
}