using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Newtonsoft.Json;

using System.Collections.Generic;

/// <summary>
/// Base for all scene objects that can be drawn or interacted with
/// </summary>
public class GameObject
{
    [JsonIgnore]
    public GameObject? Parent { get; set; }

    /// <summary>
    /// Position of the UI element on screen
    /// </summary>
    /// <value></value>
    public Vector2 Position { get; set; }

    public Vector2 GlobalPosition
    {
        get
        {
            return Position + (Parent?.GlobalPosition ?? Vector2.Zero);
        }
    }

    /// <summary>
    /// Rectangle that is used to define 
    /// </summary>
    /// <value></value>
    public Vector2 BoundingBoxSize { get; set; }

    private bool _visible = true;
    /// <summary>
    /// Should this be drawn on the screen
    /// </summary>
    public virtual bool Visible
    {
        get => _visible;
        set
        {
            _visible = value;
            foreach (GameObject child in Children)
            {
                child.Visible = value;
            }
        }
    }
    private bool _pendingKill = false;
    public bool Valid => !_pendingKill;

    [JsonIgnore]
    public readonly VisualNovelMono.VisualNovelGame? Game;
    public virtual void LoadContent(ContentManager content) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void Init() { }
    public void AddChild(GameObject child)
    {
        Children.Add(child);
        child.Visible = Visible;
        child.Parent = this;
    }
    public List<GameObject> Children { get; set; } = new List<GameObject>();

    public GameObject(VisualNovelMono.VisualNovelGame? game)
    {
        Game = game;
    }

    public virtual void Destroy()
    {
        _pendingKill = true;
        Parent?.Children.Remove(this);
        foreach (GameObject child in Children)
        {
            child.Destroy();
        }
    }
}