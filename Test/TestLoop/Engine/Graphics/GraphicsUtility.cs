using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestLoop
{
    public class GraphicsUtility
    {
        private static SpriteBatch spriteBatch;
        private static Game CurrentGame;

        public static List<Texture2D> TextureCollection = new List<Texture2D>();
        public static List<Texture2D> CompositeTextureCollection = new List<Texture2D>();

        private static List<BackgroundInformation> backgroundCollection = new List<BackgroundInformation>();
        private static List<List<GameObject>> visibleGameObjects = new List<List<GameObject>>();
        private static List<Rectangle> bufferedGameAreaRectangles = new List<Rectangle>();

        public static void Initialize(Game game)
        {
            CurrentGame = game;
            spriteBatch = new SpriteBatch(CurrentGame.GraphicsDevice);
        }

        public static int LoadTexture(string assetName)
        {
            int retr = TextureCollection.Count();
            TextureCollection.Add(CurrentGame.Content.Load<Texture2D>(assetName));
            return retr;
        }

        public static int AddTexture(Texture2D asset)
        {
            int retr = TextureCollection.Count();
            TextureCollection.Add(asset);
            CompositeTextureCollection.Add(asset);
            return retr;
        }

        public static void Draw()
        {
            CurrentGame.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
#pragma warning disable CS0618 // Type or member is obsolete

            // draw backgrounds
            foreach (BackgroundInformation back in backgroundCollection)
            {
                spriteBatch.Draw(TextureCollection[back.TextureId], back.boundGameArea.ViewportRectangle, Color.White);

                // those are the only allowed drawing locations
                if (!bufferedGameAreaRectangles.Contains(back.boundGameArea.ViewportRectangle))
                    bufferedGameAreaRectangles.Add(back.boundGameArea.ViewportRectangle);
            }

            // draw objects
            for (int i = 0; i < visibleGameObjects.Count; i++)
            {
                foreach (GameObject obj in visibleGameObjects[i])
                {
                    bool onScreen = false;

                    Rectangle clippingRectangle = obj.GetDrawingRectangle();
                    Rectangle positionRectangle = Rectangle.Empty;

                    // check for viewport clipping or offscreen objects
                    foreach (Rectangle gameAreaRec in bufferedGameAreaRectangles)
                    {
                        if (gameAreaRec.Intersects(obj.PositionRectangle))
                        {
                            onScreen = true;

                            // check if we need to clip 
                            // game ares edge clip
                            Rectangle intersectRectangle = Rectangle.Intersect(gameAreaRec, obj.PositionRectangle);

                            if (intersectRectangle.Width != obj.PositionRectangle.Width ||
                                intersectRectangle.Height != obj.PositionRectangle.Height)
                            {
                                clippingRectangle = new Rectangle(obj.GetDrawingRectangle().Location, obj.GetDrawingRectangle().Size);
                                positionRectangle = new Rectangle(obj.PositionRectangle.Location, obj.PositionRectangle.Size);

                                if (intersectRectangle.Width < obj.PositionRectangle.Width)
                                {
                                    clippingRectangle.Width = intersectRectangle.Width;
                                    positionRectangle.Width = intersectRectangle.Width;

                                    if (obj.PositionRectangle.X < gameAreaRec.X)
                                    {
                                        clippingRectangle.X += obj.PositionRectangle.Width - intersectRectangle.Width;
                                    }
                                }

                                if (intersectRectangle.Height < obj.PositionRectangle.Height)
                                {
                                    clippingRectangle.Height = intersectRectangle.Height;
                                    positionRectangle.Height = intersectRectangle.Height;

                                    if (obj.PositionRectangle.Y < gameAreaRec.Y)
                                    {
                                        clippingRectangle.Y += obj.PositionRectangle.Height - intersectRectangle.Height;
                                    }
                                }
                            }

                            // TODO : layer overlap clip
                        }
                        else if (!onScreen)
                            onScreen = gameAreaRec.Contains(obj.PositionRectangle);
                    }

                    // set visibility
                    obj.Visible = onScreen;

                    // draw if applicable
                    if (obj.Visible)
                    {
                        if (clippingRectangle == Rectangle.Empty)
                        {
                            if (positionRectangle == Rectangle.Empty)
                            {
                                spriteBatch.Draw(texture: TextureCollection[obj.textureId], destinationRectangle: obj.PositionRectangle);
                            }
                            else
                            {
                                spriteBatch.Draw(texture: TextureCollection[obj.textureId], destinationRectangle: positionRectangle);
                            }

                        }
                        else
                        {
                            if (positionRectangle == Rectangle.Empty)
                            {
                                spriteBatch.Draw(texture: TextureCollection[obj.textureId], destinationRectangle: obj.PositionRectangle, sourceRectangle: clippingRectangle);
                            }
                            else
                            {
                                spriteBatch.Draw(texture: TextureCollection[obj.textureId], destinationRectangle: positionRectangle, sourceRectangle: clippingRectangle);
                            }
                        }
                    }
                }
            }

#pragma warning restore CS0618 // Type or member is obsolete        
            spriteBatch.End();
        }

        public static GraphicsDevice CurrentGraphicsDevice
        {
            get
            {
                return CurrentGame.GraphicsDevice;
            }
        }

        public static void ClearUnmanagedContent()
        {
            // since we are creating textures but they arent handled in Mono's content
            // manager, we need to dispose them at the end.            
            foreach (Texture t in CompositeTextureCollection)
            {
                t.Dispose();
            }
        }

        private static List<GameObject> VisibleGameObjects(int layer)
        {
            if (layer < 0)
                throw new Exception("negative layer specified");

            while (layer >= visibleGameObjects.Count)
                visibleGameObjects.Add(new List<GameObject>());
            return visibleGameObjects[layer];
        }

        public static void AddBackgroundCollection(BackgroundInformation back)
        {
            if (!backgroundCollection.Contains(back))
                backgroundCollection.Add(back);
        }

        public static void RemoveBackgroundCollection(BackgroundInformation back)
        {
            if (backgroundCollection.Contains(back))
                backgroundCollection.Remove(back);
        }

        public static void AddVisibleGameObjects(int layer, GameObject obj)
        {
            List<GameObject> tempList = VisibleGameObjects(layer);
            if (!tempList.Contains(obj))
                tempList.Add(obj);
        }

        public static void RemoveVisibleGameObjects(int layer, GameObject obj)
        {
            if (layer < visibleGameObjects.Count)
            {
                List<GameObject> tempList = VisibleGameObjects(layer);
                if (!tempList.Contains(obj))
                    tempList.Add(obj);
            }
        }

    }
}
