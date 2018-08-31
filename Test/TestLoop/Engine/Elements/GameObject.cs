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
        // active flag
        public bool Active = true;

        // Visibility related
        public bool Visible = true;
        public bool OnScreen
        {
            get { return true; }
        }
        public int LayerIndex;

        // positioning 
        private Vector2 screenPosition = new Vector2();
        public float X
        {
            get { return screenPosition.X; }
            set { screenPosition.X = value; PositionRectangle.X = Convert.ToInt32(value); }
        }
        public float Y
        {
            get { return screenPosition.Y; }
            set { screenPosition.Y = value; PositionRectangle.Y = Convert.ToInt32(value); }
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
        public abstract void Update();

        public virtual Rectangle GetDrawingRectangle()
        {
            return Rectangle.Empty;
        }
    }
}
