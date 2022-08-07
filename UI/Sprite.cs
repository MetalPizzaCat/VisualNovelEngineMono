using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using VisualNovelMono;

public class Sprite : UI.UserInterfaceElement
{
    private Texture2D? _texture = null;
    private Vector2 _scale = Vector2.One;
    public Color Color { get; set; } = Color.White;

    public Vector2 Size { get; set; }

    public Texture2D? Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            _scale = new Vector2(Size.X / _texture.Width, Size.Y / _texture.Height);
        }
    }

    public Sprite(Texture2D texture, Vector2 position, Vector2 size, VisualNovelGame game) : base(position, size, game)
    {
        Size = size;
        Texture = texture;
    }

    public Sprite(Vector2 position, Vector2 size, VisualNovelGame game) : base(position, size, game)
    {
        Size = size;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (_texture != null)
        {
            spriteBatch.Draw(_texture, new Rectangle
                (
                    (GlobalPosition * Game.Scale).ToPoint(), (BoundingBoxSize * Game.Scale * _scale).ToPoint()
                ), null, Color);
        }
    }
}