using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UI
{
    /// <summary>
    /// Represents base of all of the ui elements
    /// Has location and bounding box that are used for mouse detection
    /// </summary>
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

        public virtual void LoadContent(ContentManager content)
        {
            
        }

        public virtual void Draw(SpriteBatch batch)
        {

        }
    }
}