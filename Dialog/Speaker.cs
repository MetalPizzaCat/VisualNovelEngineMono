using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class Speaker : GameObject
{
    public string? DisplayName { get; set; }
    public string? DisplayTextureName { get; set; }
    public bool Active { get; set; } = false;
    /// <summary>
    /// Position of the speaker on the screen
    /// </summary>
    public Vector2 Position { get; set; } = Vector2.Zero;

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (_texture != null)
        {
            spriteBatch.Draw(_texture, Position, null, Active ? Color.White : Color.LightGray);
        }
    }

    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);
        if (DisplayTextureName != null)
        {
            _texture ??= content.Load<Texture2D>(DisplayTextureName);
        }
    }

    public Speaker(string? name, string? textureName)
    {
        DisplayName = name;
        DisplayTextureName = textureName;
    }
    public Speaker(string? name, Texture2D? texture)
    {
        DisplayName = name;
        _texture = texture;
    }

    private Texture2D? _texture;
}