using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class GameArea
    {
        public Vector2 ScreenPosition;
        public int Width;
        public int Height;
        public int ViewportWidth;
        public int ViewportHeight;
        public Rectangle ViewportRectangle;

        private List<GameObject> registeredObjects = new List<GameObject>();
        private List<GameObject> activeObjects = new List<GameObject>();

        public BackgroundInformation Background { get; } 

        public GameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight)
        {
            ScreenPosition = new Vector2(screenPositionX, screenPositionY);
            ViewportHeight = viewportHeight;
            ViewportWidth = viewportWidth;

            Background = new BackgroundInformation(this);
            ViewportRectangle = new Rectangle(screenPositionX, screenPositionY, ViewportWidth, ViewportHeight);
        }

        public void AddGameObject(GameObject newObject)
        {
            // Init object
            newObject.Initialize();

            // Populate collections
            registeredObjects.Add(newObject);            

            if (newObject.Active)
            {
                activeObjects.Add(newObject);
            }            
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update()
        {
            for (int i = activeObjects.Count - 1; i >= 0; i--)
            {
                activeObjects[i].Update();
            }
        }
    }
}
