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
        public Texture2D? ButtonTextureAtlas { get; set; }
        public Rectangle? TextureSourceRectangle;

        private string? _text;
        public string? Text
        {
            get => _text;
            set
            {
                if (Font != null && value != null)
                {
                    Vector2 bBoxSize = Font.MeasureString(value);
                    //this way we ensure that bounding box is big enough
                    // for text and tries to stay as big as user defined it
                    Vector2 res = new Vector2
                    (
                        bBoxSize.X > BoundingBoxSize.X ? bBoxSize.X : BoundingBoxSize.X,
                        bBoxSize.Y > BoundingBoxSize.Y ? bBoxSize.Y : BoundingBoxSize.Y
                    );
                    BoundingBoxSize = res;
                }
                _text = value;
            }
        }
        /// <summary>
        /// Font that will be used to display text
        /// Note: because of how monogame handles assets you can call "load" as much as you want
        /// but you will still load one instance of the asset
        /// </summary>
        public SpriteFont? Font { get; set; }

        public Button(VisualNovelMono.VisualNovelGame game, Vector2 position, Vector2 size, Rectangle? srcRect = null) : base(position, size, game)
        {
            TextureSourceRectangle = srcRect ?? ButtonTextureAtlas?.Bounds;
        }

        public Button
        (
            VisualNovelMono.VisualNovelGame game,
            string text,
            Vector2 position,
            Vector2 size,
            Rectangle? srcRect = null
        ) : base(position, size, game)
        {
            TextureSourceRectangle = srcRect ?? ButtonTextureAtlas?.Bounds;
            _text = text;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            if (Visible)
            {
                batch.Draw(ButtonTextureAtlas, new Rectangle
                (
                    (Position * Game.Scale).ToPoint(), (BoundingBoxSize * Game.Scale).ToPoint()
                ), TextureSourceRectangle, Color.White);

                batch.DrawString(Font, Text ?? "no text lol", Position * Game.Scale, Color.White);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            Font = content.Load<SpriteFont>("Roboto");
            ButtonTextureAtlas ??= content.Load<Texture2D>("UI/transparent_button");
            Text = _text;
        }
    }
}