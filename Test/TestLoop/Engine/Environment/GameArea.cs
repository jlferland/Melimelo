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
        public float GravitationalConstant { get; set; } = 9.8f;
        public float AirDensity { get; set; } = 0.5f;
        public Direction GravityDirection { get; } = new Direction();

        public float PixelsPerMeters { get; set; } = 10;        
        public float MaxFramePerSecond { get; set; } = 60; // impacts event clock to set new events triggers

        public GravityHandler Gravity { get; set; }
        public CollisionHandler Collision { get; set; }

        public Vector2 ScreenPosition { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        public Rectangle ViewportRectangle { get; set; }

        public BackgroundInformation Background { get; }

        private List<GameObject> registeredObjects = new List<GameObject>();
        private List<GameObject> activeObjects = new List<GameObject>();
        
        public GameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight)
        {
            GravityDirection.Value = Direction.SOUTH;

            ScreenPosition = new Vector2(screenPositionX, screenPositionY);
            ViewportHeight = viewportHeight;
            ViewportWidth = viewportWidth;

            Background = new BackgroundInformation(this);
            ViewportRectangle = new Rectangle(screenPositionX, screenPositionY, ViewportWidth, ViewportHeight);
        }

        public void AddGameObject(params GameObject[] newObjects)
        {
            foreach (GameObject newObject in newObjects)
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
        }

        public virtual void Initialize()
        {
            Gravity = new GravityHandler(GravitationalConstant, GravityDirection.Value, AirDensity);
            Collision = new CollisionHandler(this);
        }

        public virtual void Update()
        {
            // move objects
            for (int i = activeObjects.Count - 1; i >= 0; i--)
            {
                activeObjects[i].Update();
            }
            
            // gravity
            Gravity.Apply();

            // handle collisions
            Collision.HandleCollisions();
        }
    }
}
