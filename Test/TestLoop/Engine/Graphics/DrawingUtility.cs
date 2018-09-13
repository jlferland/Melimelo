using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestLoop
{
    public class DrawingUtility
    {
        public static Dictionary<Color, int> Dots = new Dictionary<Color, int>();

        public static int GetColoredDotTextureId(Color color)
        {
            int textureId;

            if (!Dots.TryGetValue(color, out textureId))
            {
                Texture2D texture;
                texture = new Texture2D(GraphicsUtility.CurrentGraphicsDevice, 1, 1);
                texture.SetData(new Color[] { color });
                textureId = GraphicsUtility.AddTexture(color.ToString(), texture);
                Dots.Add(color, textureId);
            }

            return textureId;
        }      
    }
}
