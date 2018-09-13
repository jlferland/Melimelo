using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public abstract class GameObject
    {
        // Current game area
        public GameArea CurrentGameArea { get; set; }

        // active flag
        public bool Active { get; set; } = true;

        // Visibility related
        public bool Visible { get; set; } = true;
        public bool OnScreen
        {
            get { return true; }
        }
        public int LayerIndex;

        // positioning 
        private Vector2 ScreenPosition = new Vector2();

        public float X
        {
            get { return ScreenPosition.X; }
            set { ScreenPosition.X = value; PositionRectangle.X = Convert.ToInt32(value); }
        }
        public float Y
        {
            get { return ScreenPosition.Y; }
            set { ScreenPosition.Y = value; PositionRectangle.Y = Convert.ToInt32(value); }
        }

        public Vector2 GameAreaPosition = new Vector2();

        public Rectangle PositionRectangle = Rectangle.Empty;

        // size related
        public short Width;
        public short Height;

        // texture
        public int textureId;
        public string assetName;

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);

        public GameObject(GameArea currentGameArea)
        {
            CurrentGameArea = currentGameArea;
        }

        public virtual Rectangle GetDrawingRectangle()
        {
            return Rectangle.Empty;
        }
    }
}
