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
        public string? Text { get; set; }
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
            Text = text;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            if (Visible)
            {
                batch.Draw(ButtonTextureAtlas, new Rectangle(Position.ToPoint(), BoundingBoxSize.ToPoint()), TextureSourceRectangle, Color.White);
                batch.DrawString(Font, Text ?? "no text lol", Position, Color.White);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            Font = content.Load<SpriteFont>("Roboto");
            ButtonTextureAtlas ??= content.Load<Texture2D>("buttons_small");
        }
    }
}