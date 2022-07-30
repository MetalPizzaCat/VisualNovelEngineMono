using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

/// <summary>
/// Base for all scene objects that can be drawn or interacted with
/// </summary>
public class GameObject
{
    [JsonIgnore]
    public readonly VisualNovelMono.VisualNovelGame? Game;
    public virtual void LoadContent(ContentManager content) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void Init() { }

    public GameObject(VisualNovelMono.VisualNovelGame? game)
    {
        Game = game;
    }
}