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
            set { ScreenPosition.X = value; positionRectangle.X = Convert.ToInt32(value); }
        }
        public float Y
        {
            get { return ScreenPosition.Y; }
            set { ScreenPosition.Y = value; positionRectangle.Y = Convert.ToInt32(value); }
        }

        public Vector2 GameAreaPosition = new Vector2();

        public Rectangle positionRectangle = Rectangle.Empty;
        public Rectangle PositionRectangle
        {
            get { return positionRectangle; }
            set { positionRectangle = value; ReScale(); }
        }

        // size related
        public int Width;
        public int Height;

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

        public virtual void DeScale()
        {
            X = (X - CurrentGameArea.ViewportRectangle.X) / CurrentGameArea.Scale;
            Y = (Y - CurrentGameArea.ViewportRectangle.Y) / CurrentGameArea.Scale;
            positionRectangle.Width = Convert.ToInt32(Width / CurrentGameArea.Scale);
            positionRectangle.Height = Convert.ToInt32(Height /CurrentGameArea.Scale);
        }

        public virtual void ReScale()
        {
            X = (X * CurrentGameArea.Scale) + CurrentGameArea.ViewportRectangle.X;
            Y = (Y * CurrentGameArea.Scale) + CurrentGameArea.ViewportRectangle.Y;
            positionRectangle.Width = Convert.ToInt32(CurrentGameArea.Scale * Width);
            positionRectangle.Height = Convert.ToInt32(CurrentGameArea.Scale * Height);
        }
    }
}
