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
        public static List<GameObject> VisibleGameObjects = new List<GameObject>();

        public static void Initialize(Game game)
        {
            CurrentGame = game;
            spriteBatch = new SpriteBatch(CurrentGame.GraphicsDevice);
        }

        public static int LoadTexture(string assetName, GameObject gameObject)
        {
            int retr = TextureCollection.Count();
            TextureCollection.Add(CurrentGame.Content.Load<Texture2D>(assetName));
            return retr;            
        }

        public static void Draw()
        {
            CurrentGame.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (GameObject obj in VisibleGameObjects)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                spriteBatch.Draw(texture: TextureCollection[obj.textureId], position: obj.Position, sourceRectangle: obj.GetDrawingRectangle());
#pragma warning restore CS0618 // Type or member is obsolete
            }

            spriteBatch.End();
        }
    }
}
