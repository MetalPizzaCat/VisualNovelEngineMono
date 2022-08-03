using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using DialogSystem;

using Newtonsoft.Json;

using System.Collections.Generic;

/// <summary>
/// Represents speaker on the screen
/// </summary>
public class Speaker : UI.UserInterfaceElement
{
    public bool Active { get; set; } = false;
    /// <summary>
    /// Position of the speaker on the screen
    /// </summary>
    public Vector2 Position { get; set; } = Vector2.Zero;
    public SpeakerData SpeakerData { get; set; }
    public Dictionary<string, Texture2D> StateTextures { get; set; } = new Dictionary<string, Texture2D>();
    public string Name { get; set; } = "DEFAULT";
    private Texture2D? _texture;

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        if (_texture != null)
        {
            spriteBatch.Draw(_texture, new Rectangle(Position.ToPoint(), BoundingBoxSize.ToPoint()), null, Active ? Color.White : Color.LightGray);
        }
    }

    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);
        foreach (KeyValuePair<string, string> texture in SpeakerData.TextureStateNames)
        {
            StateTextures.Add(texture.Key, content.Load<Texture2D>(texture.Value));
        }
        _texture = StateTextures[SpeakerData.DefaultTextureName];
    }

    public Speaker(SpeakerData data, string name, VisualNovelMono.VisualNovelGame game) : base(Vector2.Zero, new Vector2(128, 128), game)
    {
        SpeakerData = data;
        Name = name;
    }
}