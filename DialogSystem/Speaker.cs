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
    public delegate void BeginMovementEventHandler();
    public delegate void EndMovementEventHandler();

    public event BeginMovementEventHandler? OnBegunMovement;
    public event EndMovementEventHandler? OnFinishedMovement;

    private Dialog _dialog;
    private Sprite _sprite;
    private Vector2 _targetPosition;
    private bool _moving = false;
    private float _speed = 1;

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

    public void Move(Vector2 newPosition)
    {
        if (Position != newPosition)
        {
            _targetPosition = newPosition;
            _speed = (_targetPosition.X - Position.X) < 0 ? -10 : 10;
            _moving = true;
            OnBegunMovement?.Invoke();
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
        _sprite.Layer = UI.RenderLayer.Speakers;
        game.AddUiElement(_sprite);
        AddChild(_sprite);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (_moving)
        {
            Position += new Vector2(_speed, 0);
            if (System.MathF.Abs(Position.X - _targetPosition.X) <= System.MathF.Abs(_speed))
            {
                _moving = false;
                OnFinishedMovement?.Invoke();
            }
        }
    }
}