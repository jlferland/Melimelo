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
        public int TextureId { get; }

        private List<Rectangle> SpriteFrames { get; } = new List<Rectangle>();
        private int MaxFrame { get; set; }
        private int currentFrame;

        public SpriteSheetHandler(int objTextureId, int startOffsetX, int startOffsetY, int offsetX, int offsetY, int width, int height)
        {
            TextureId = objTextureId;
            int imageHeight = GraphicsUtility.TextureCollection[TextureId].Height;
            int imageWidth = GraphicsUtility.TextureCollection[TextureId].Width;
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
            MaxFrame = SpriteFrames.Count - 1;
        }

        public int CurrentFrame
        {
            set {
                if (value < 0)
                    CurrentFrame = 0;
                else if (value > SpriteFrames.Count - 1)
                    CurrentFrame = MaxFrame;
                else
                    CurrentFrame = value;
            }
            get { return CurrentFrame; }
        }

        public Rectangle GetFrameRectangle(int id)
        {
            if (id < 0)
                id = 0;
            else if (id > SpriteFrames.Count - 1)
                id = MaxFrame;

            return SpriteFrames[id];
        }

        public Rectangle getCurrentFrameRectangle()
        {
            return SpriteFrames[CurrentFrame];
        }

        public int Count
        {
            get
            {
                return SpriteFrames.Count;
            }
        }
    }
}
