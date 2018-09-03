using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class BackgroundInformation
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; } = Color.CornflowerBlue;
        public int TextureId { get; set; } = -1;

        public GameArea boundGameArea;

        public BackgroundInformation(GameArea currentGameArea)
        {
            boundGameArea = currentGameArea;

            TextureId = DrawingUtility.GetColoredDotTextureId(Color.CornflowerBlue);

            GraphicsUtility.AddBackgroundCollection(this);
        }
    }
}
