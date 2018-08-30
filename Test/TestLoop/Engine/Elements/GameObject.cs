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
        public Vector2 ScreenPosition = new Vector2();
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
