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
        private int textureId;

        public SpriteSheetHandler(int objTextureId, int startOffsetX, int startOffsetY, int offsetX, int offsetY, int width, int height)
        {
            textureId = objTextureId;
            int imageHeight = GraphicsUtility.TextureCollection[textureId].Height;
            int imageWidth = GraphicsUtility.TextureCollection[textureId].Width;
            int currentRow = startOffsetY;
            int currentColumn = startOffsetX;

            while (true)
            {
                if (currentColumn > imageWidth)
                {
                    currentRow += height + offsetY + startOffsetY;
                    currentColumn = startOffsetX;
                }

                if (currentRow > imageHeight)
                    break;

                SpriteFrames.Add(new Rectangle(currentColumn, currentRow, width, height));

                currentColumn += width + offsetX + startOffsetX;
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

        public Rectangle GetFrameRectangle(int id)
        {
            if (id < 0)
                id = 0;
            else if (id > SpriteFrames.Count - 1)
                id = maxFrame;

            return SpriteFrames[id];
        }

        public Rectangle getCurrentFrameRectangle()
        {
            return SpriteFrames[currentFrame];
        }

        public int Count
        {
            get
            {
                return SpriteFrames.Count;
            }
        }

        public int TextureId
        {
            get
            {
                return textureId;
            }
        }
    }
}
