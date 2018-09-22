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
        private float scale = 1;
        public float Scale
        {
            get { return scale; }
            set
            {
                if (scale <= 0)
                    scale = 0.1f;

                DeScaleAll();
                scale = value;
                ReScaleAll();
            }
        }

        private int maxFramePerSecond = 60;
        public int MaxFramePerSecond // impacts event clock to set new events triggers
        {
            get { return maxFramePerSecond; }
            set
            {
                maxFramePerSecond = value;
                NextFrameTimeSpan = new TimeSpan(0, 0, 0, 0, (int)(((float)1 / value) * 1000));
            }
        }
        private TimeSpan NextFrameTimeSpan { get; set; } = new TimeSpan(0, 0, 0, 0, (int)(((float)1 / 60) * 1000));

        public GravityHandler Gravity { get; set; }
        public CollisionHandler Collision { get; set; }

        public Vector2 ScreenPosition { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        private int ViewportWidthOneHalf { get; set; }
        private int ViewportHeightOneHalf { get; set; }
        public int TotalWidth { get; private set; }
        public int TotalHeight { get; private set; }

        public Rectangle ViewportRectangle { get; set; }

        public BackgroundInformation Background { get; }

        public GameAreaEdgeBehavior EdgeBehaviorX { get; set; }
        public GameAreaEdgeBehavior EdgeBehaviorY { get; set; }
        public bool XScroll { get; set; }
        public bool YScroll { get; set; }

        private List<GameObject> registeredObjects = new List<GameObject>();
        private List<GameObject> activeObjects = new List<GameObject>();

        private GameObject pinnedGameObject;
        private bool pinnedGameObjectActive;
        private int pinnedObjectAbsolutePositionX = 0;
        private int pinnedObjectAbsolutePositionY = 0;

        public GameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight)
        {
            GravityDirection.Value = Direction.SOUTH;

            ScreenPosition = new Vector2(screenPositionX, screenPositionY);
            ViewportHeight = viewportHeight;
            ViewportWidth = viewportWidth;
            ViewportWidthOneHalf = ViewportWidth / 2;
            ViewportHeightOneHalf = ViewportHeight / 2;
            TotalWidth = ViewportWidth;
            TotalHeight = ViewportHeight;


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

                // Adjust total width and height
                int newX = newObject.positionRectangle.X + newObject.Width;
                int newY = newObject.positionRectangle.Y + newObject.Height;
                if (TotalWidth < newX) TotalWidth = newX;
                if (TotalWidth < newY) TotalHeight = newY;
            }
        }

        public virtual void Initialize()
        {
            Gravity = new GravityHandler(GravitationalConstant, GravityDirection.Value, AirDensity);
            Collision = new CollisionHandler(this);

            // register first event
            EventClock.AddGameEvent(new EventClock.GameEvent(Update), TimeSpan.Zero);
        }

        public virtual void Update(GameTime gameTime)
        {
            // readjust objects according to viewport settings
            int pinnedOffsetX = 0;
            int pinnedOffsetY = 0;

            if (pinnedGameObjectActive && 
                pinnedGameObject != null)
            {
                int originalX = pinnedGameObject.PositionRectangle.X;
                int originalY = pinnedGameObject.PositionRectangle.Y;

                // gravity and update
                Gravity.Apply(gameTime);
                pinnedGameObject.Update(gameTime);

                // check for unpinning
                pinnedGameObjectActive = (XScroll && !((pinnedObjectAbsolutePositionX <= ViewportWidthOneHalf || pinnedObjectAbsolutePositionX + pinnedGameObject.Width >= TotalWidth - 100)));

                // calculate offset
                // put the object back to its original position
                if (XScroll)
                {
                    pinnedOffsetX = (pinnedGameObject.PositionRectangle.X - originalX) * -1;
                    if (!pinnedGameObjectActive)
                    {
                        // adjust offset
                    }

                    pinnedGameObject.X += pinnedOffsetX;
                    pinnedObjectAbsolutePositionX -= pinnedOffsetX;
                }
                if (YScroll)
                { 
                    pinnedOffsetY = (pinnedGameObject.PositionRectangle.Y - originalY) * -1;
                    if (!pinnedGameObjectActive)
                    {
                        // adjust offset
                    }

                    pinnedGameObject.Y += pinnedOffsetY;
                    pinnedObjectAbsolutePositionY -= pinnedOffsetY;
                }
            }
            else
            {
                // gravity
                Gravity.Apply(gameTime);

                // check for pinning
                if (pinnedGameObject != null)
                {
                    // check if we need to pin or unpin the player
                    pinnedGameObjectActive = (XScroll && (pinnedGameObject.PositionRectangle.X > 100 && pinnedGameObject.PositionRectangle.X + pinnedGameObject.Width < 200));
                    if (pinnedGameObjectActive)
                    {
                        if (pinnedObjectAbsolutePositionX == 0)
                            pinnedObjectAbsolutePositionX = pinnedGameObject.PositionRectangle.X;
                        if (pinnedObjectAbsolutePositionY == 0)
                        pinnedObjectAbsolutePositionY = pinnedGameObject.PositionRectangle.Y;
                    }
                }
            }

            // move objects / user input
            for (int i = activeObjects.Count - 1; i >= 0; i--)
            {
                if (pinnedGameObject == null || !pinnedGameObjectActive)
                {
                    activeObjects[i].Update(gameTime);
                }
                else if (pinnedGameObject != activeObjects[i])
                {
                    // if an object is pinned, don't process it
                    activeObjects[i].Update(gameTime);

                    if (XScroll)
                        activeObjects[i].X += pinnedOffsetX;
                    if (YScroll)
                        activeObjects[i].Y += pinnedOffsetY;
                }

                // GameArea Edge processing 

            }

            // handle collisions
            Collision.HandleCollisions();
            
            // background processing 

            // register next update
            EventClock.AddGameEvent(new EventClock.GameEvent(Update), NextFrameTimeSpan);
        }

        public virtual void DeScaleAll()
        {
            // move objects / user input
            foreach (GameObject obj in registeredObjects)
            {
                obj.DeScale();
            }
        }

        public virtual void ReScaleAll()
        {
            // move objects / user input
            foreach (GameObject obj in registeredObjects)
            {
                obj.ReScale();
            }
        }

        public void PinViewportOnObject(GameObject objectToPin)
        {
            pinnedGameObject = objectToPin;
            pinnedObjectAbsolutePositionX = 0;
            pinnedObjectAbsolutePositionY = 0;
            pinnedGameObjectActive = false;
        }

        public void UnPinViewporObject()
        {
            pinnedGameObject = null;
            pinnedObjectAbsolutePositionX = 0;
            pinnedObjectAbsolutePositionY = 0;
            pinnedGameObjectActive = false;
        }
    }
}
