using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestLoop
{
    public class TiledTextureRenderer
    {
        private List<int> TextureIds = new List<int>();
        private Dictionary<int, Rectangle> TextureRectangles = new Dictionary<int, Rectangle>();

        public void AddTextureId(int id)
        {
            TextureIds.Add(id);
        }

        public void AddTextureId(int[] ids)
        {
            TextureIds.AddRange(ids);
        }

        public void AddTextureId(int id, Rectangle sourceRectangle)
        {
            TextureRectangles.Add(TextureIds.Count, sourceRectangle);
            TextureIds.Add(id);
        }

        public void AddTextureId(int[] ids, Rectangle[] sourceRectangle)
        {
            if (ids.Length != sourceRectangle.Length)
                throw new Exception("id and sourcerectangle count mismatch");

            for (int i = 0; i < ids.Length ; i++)
            {
                AddTextureId(ids[i], sourceRectangle[i]);
            }
        }

        public void AddTextureId(SpriteSheetHandler sprites)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                TextureRectangles.Add(TextureIds.Count, sprites.GetFrameRectangle(i));
                TextureIds.Add(sprites.TextureId);
            }
        }

        public int RenderTexture(string assetName, List<TiledTextureDescriptor> textureDescriptors, int width, int height)
        {
            int newTextureId = GraphicsUtility.TextureCollection.Count;

            SpriteBatch spriteBatch = new SpriteBatch(GraphicsUtility.CurrentGraphicsDevice);
            RenderTarget2D renderTarget = new RenderTarget2D(GraphicsUtility.CurrentGraphicsDevice, width, height); // TODO: figure out image size
            
            GraphicsUtility.CurrentGraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsUtility.CurrentGraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin();

            foreach (TiledTextureDescriptor ttd in textureDescriptors)
            {
                Rectangle ttdr;
#pragma warning disable CS0618 // Type or member is obsolete
                if (TextureRectangles.TryGetValue(ttd.TiledTextureId, out ttdr))
                {
                    spriteBatch.Draw(texture: GraphicsUtility.TextureCollection[TextureIds[ttd.TiledTextureId]],
                                     position: ttd.position, 
                                     sourceRectangle: ttdr);
                }
                else
                {
                    spriteBatch.Draw(texture: GraphicsUtility.TextureCollection[TextureIds[ttd.TiledTextureId]],
                                     position: ttd.position);
                }
#pragma warning restore CS0618 // Type or member is obsolete
            }

            spriteBatch.End();

            GraphicsUtility.CurrentGraphicsDevice.SetRenderTarget(null);

            newTextureId = GraphicsUtility.AddTexture(assetName, renderTarget);

            return newTextureId;
        }

    }
}
