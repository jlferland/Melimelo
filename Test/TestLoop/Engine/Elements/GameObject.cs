using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class GameObject
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
        public Vector3 Position;

        // size related
        public short Width;
        public short Height;

        // texture
        public int textureId;
        public string assetName;
    }
}
