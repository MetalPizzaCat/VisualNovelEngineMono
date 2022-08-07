using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UI
{
    public enum RenderLayer : uint
    {
        Default,
        InteractionUI,
        Speakers,
        Background,
        MAX
    }

    /// <summary>
    /// Represents base of all of the ui elements
    /// Has location and bounding box that are used for mouse detection
    /// </summary>
    public class UserInterfaceElement : GameObject
    {
        public delegate void ClickEventHandler(UserInterfaceElement sender);
        public delegate void HoverEventHandler();
        public delegate void UnhoverEventHandler();

        public event ClickEventHandler? OnClicked;
        public event HoverEventHandler? OnHoveredOver;
        public event UnhoverEventHandler? OnUnhovered;

        public RenderLayer Layer = RenderLayer.Default;

        public float RenderDepthLayer => (float)Layer / (float)RenderLayer.MAX;

        /// <summary>
        /// Function that calls of the event and handles all of the cosmetics<br/>
        /// Ui element must be visible to be processed
        /// </summary>
        public virtual void Click()
        {
            if (Visible)
            {
                OnClicked?.Invoke(this);
            }
        }

        public UserInterfaceElement(Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(game)
        {
            Position = position;
            BoundingBoxSize = size;
        }
    }
}