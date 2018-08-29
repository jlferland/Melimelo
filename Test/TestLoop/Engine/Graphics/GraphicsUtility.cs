using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TestLoop
{
    public class GraphicsUtility
    {
        public static List<Texture> TextureCollection = new List<Texture>();

        public static int LoadTexture(string assetName)
        {
            int retr = TextureCollection.Count();
            return retr;
        }
    }
}
