using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class TiledTextureDescriptor
    {
        public Vector2 position = new Vector2();
        public int TiledTextureId;
        public bool PixelTexturePlacement { get => PixelTexturePlacement; set => PixelTexturePlacement = value; }

        public TiledTextureDescriptor(int X, int Y, int tiledTextureId)
        {
            position.X = X;
            position.Y = Y;
            TiledTextureId = tiledTextureId;
        }
    }
}
