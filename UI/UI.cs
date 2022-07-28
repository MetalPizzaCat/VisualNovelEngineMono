using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UserInterfaceElement
{
    /// <summary>
    /// Position of the UI element on screen
    /// </summary>
    /// <value></value>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Rectangle that is used to define 
    /// </summary>
    /// <value></value>
    public Vector2 BoundingBoxSize { get; set; }

    public delegate void ClickEventHandler();
    public delegate void HoverEventHandler();
    public delegate void UnhoverEventHandler();

    public event ClickEventHandler OnClicked;
    public event HoverEventHandler OnHoveredOver;
    public event UnhoverEventHandler OnUnhovered;

    /// <summary>
    /// Function that calls of the event and handles all of the cosmetics
    /// </summary>
    public virtual void Click()
    {
        OnClicked.Invoke();
    }

    public UserInterfaceElement(Vector2 position, Vector2 size)
    {
        Position = position;
        BoundingBoxSize = size;
    }

    public virtual void Draw(SpriteBatch batch)
    {

    }
}

/// <summary>
/// Generic button, that has build in support for changing texture based on state
/// </summary>
public class Button : UserInterfaceElement
{
    private Texture2D _buttonTextureAtlas;
    private Rectangle _sourceRectangle;

    public Button(Vector2 position, Vector2 size, Texture2D buttonAtlas, Rectangle? srcRect = null) : base(position, size)
    {
        _buttonTextureAtlas = buttonAtlas;
        _sourceRectangle = srcRect ?? _buttonTextureAtlas.Bounds;
    }

    public override void Draw(SpriteBatch batch)
    {
        base.Draw(batch);
        batch.Draw(_buttonTextureAtlas, new Rectangle(Position.ToPoint(), BoundingBoxSize.ToPoint()), _sourceRectangle, Color.White);
    }
}