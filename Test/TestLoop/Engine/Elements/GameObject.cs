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

        // positioning 
        public Vector2 Position = new Vector2();

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
