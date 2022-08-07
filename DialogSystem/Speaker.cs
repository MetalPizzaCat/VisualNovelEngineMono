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
    private Dialog _dialog;
    private Sprite _sprite;
    public bool Active { get; set; } = false;
    private SpeakerPosition _scenePosition = SpeakerPosition.Offscreen;
    public SpeakerPosition ScenePosition
    {
        get => _scenePosition;
        set
        {
            _scenePosition = value;
        }
    }
    public SpeakerData SpeakerData { get; set; }
    public Dictionary<string, Texture2D> StateTextures { get; set; } = new Dictionary<string, Texture2D>();
    public string Name { get; set; } = "DEFAULT";

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public override void LoadContent(ContentManager content)
    {
        base.LoadContent(content);
        foreach (KeyValuePair<string, string> texture in SpeakerData.TextureStateNames)
        {
            StateTextures.Add(texture.Key, content.Load<Texture2D>(texture.Value));
        }
        _sprite.Texture = StateTextures[SpeakerData.DefaultTextureName];
    }

    public override void Init()
    {
        base.Init();
    }

    public Speaker(Dialog dialog, Vector2 size, SpeakerPosition position, SpeakerData data, string name, VisualNovelMono.VisualNovelGame game) : base(Vector2.Zero, new Vector2(128, 128), game)
    {
        SpeakerData = data;
        Name = name;
        _dialog = dialog;
        _scenePosition = position;

        _sprite = new Sprite(Vector2.Zero, size, game);
        game.AddUiElement(_sprite);
        AddChild(_sprite);
    }
}