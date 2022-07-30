using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UI
{
    /// <summary>
    /// Generic button, that has build in support for changing texture based on state
    /// </summary>
    public class Button : UserInterfaceElement
    {
        private Texture2D? _buttonTextureAtlas;
        private Rectangle _sourceRectangle;
        private string? _text;
        /// <summary>
        /// Font that will be used to display text
        /// Note: because of how monogame handles assets you can call "load" as much as you want
        /// but you will still load one instance of the asset
        /// </summary>
        private SpriteFont? _font;

        public string? Text
        {
            get => _text;
            set => _text = value;
        }

        public Button(VisualNovelMono.VisualNovelGame game, Vector2 position, Vector2 size, Rectangle? srcRect = null) : base(position, size, game)
        {
            _sourceRectangle = srcRect ?? _buttonTextureAtlas?.Bounds ?? new Rectangle();
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            batch.Draw(_buttonTextureAtlas, new Rectangle(Position.ToPoint(), BoundingBoxSize.ToPoint()), _sourceRectangle, Color.White);
            batch.DrawString(_font, _text ?? "no text lol", Position, Color.White);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _font = content.Load<SpriteFont>("Roboto");
            _buttonTextureAtlas ??= content.Load<Texture2D>("buttons_small");
        }
    }
}