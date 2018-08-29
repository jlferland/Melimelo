using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class SpriteSheetHandler
    {
        private List<Rectangle> SpriteFrames = new List<Rectangle>();
        private int maxFrame;
        private int currentFrame;

        public SpriteSheetHandler(GameObject obj, int startOffsetX, int startOffsetY, int offsetX, int offsetY)
        {
            int imageHeight = GraphicsUtility.TextureCollection[obj.textureId].Height;
            int imageWidth = GraphicsUtility.TextureCollection[obj.textureId].Width;
            int currentRow = startOffsetY;
            int currentColumn = startOffsetX;

            while (true)
            {
                if (currentColumn > imageWidth)
                {
                    currentRow += obj.Height + offsetY + startOffsetY;
                    currentColumn = startOffsetX;
                }

                if (currentRow > imageHeight)
                    break;

                SpriteFrames.Add(new Rectangle(currentColumn, currentRow, obj.Width, obj.Height));

                currentColumn += obj.Width + offsetX + startOffsetX;
            }
            maxFrame = SpriteFrames.Count - 1;
        }

        public int CurrentFrame
        {
            set {
                if (value < 0)
                    currentFrame = 0;
                else if (value > SpriteFrames.Count - 1)
                    currentFrame = maxFrame;
                else
                    currentFrame = value;
            }
            get { return currentFrame; }
        }

        public Rectangle getCurrentFrameRectangle()
        {
            return SpriteFrames[currentFrame];
        }
    }
}
