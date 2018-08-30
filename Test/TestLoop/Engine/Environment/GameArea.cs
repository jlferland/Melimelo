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
        private List<GameObject> registeredObjects = new List<GameObject>();
        private List<GameObject> activeObjects = new List<GameObject>();

        private Vector2 ScreenPosition;
        private int Width;
        private int Height;
        private int ViewportWidth;
        private int ViewportHeight;



        public GameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight)
        {
            ScreenPosition = new Vector2(screenPositionX, screenPositionY);
            ViewportHeight = viewportHeight;
            ViewportWidth = viewportWidth;
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
