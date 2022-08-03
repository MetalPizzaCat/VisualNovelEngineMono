using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UI
{
    /// <summary>
    /// Generic button, that has build in support for changing texture based on state
    /// </summary>
    public class Label : UserInterfaceElement
    {
        private SpriteFont? _font;
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                Vector2 bBoxSize = _font?.MeasureString(Text) ?? Vector2.Zero;
                //this way we ensure that bounding box is big enough
                // for text and tries to stay as big as user defined it
                Vector2 res = new Vector2
                (
                    bBoxSize.X > BoundingBoxSize.X ? bBoxSize.X : BoundingBoxSize.X,
                    bBoxSize.Y > BoundingBoxSize.Y ? bBoxSize.Y : BoundingBoxSize.Y
                );
                BoundingBoxSize = res;
            }
        }
        public Label(VisualNovelMono.VisualNovelGame game, string text, Vector2 position) : base(position, Vector2.Zero, game)
        {
            Text = text;
        }
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _font = content.Load<SpriteFont>("Roboto");
            Vector2 bBoxSize = _font.MeasureString(Text);
            //this way we ensure that bounding box is big enough
            // for text and tries to stay as big as user defined it
            Vector2 res = new Vector2
            (
                bBoxSize.X > BoundingBoxSize.X ? bBoxSize.X : BoundingBoxSize.X,
                bBoxSize.Y > BoundingBoxSize.Y ? bBoxSize.Y : BoundingBoxSize.Y
            );
            BoundingBoxSize = res;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Visible)
            {
                spriteBatch.DrawString(_font, Text, Position, Color.White);
            }
        }
    }
}