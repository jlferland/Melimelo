using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestLoop
{
    public class Player : GameObject, ICollidable, IMobile, IUserInputtable
    {
        // movement related
        public Direction Direction { get; } = new Direction();
        public float Velocity { get; set; }
        public float MaximumVelocity { get; set; }
        public float Acceleration { get; set; }
        public bool Stopped {
            get => Velocity == 0;
            set { Velocity = 0; Acceleration = 0; Direction.Value = Direction.NOVALUE; }
        }

        // gravity related
        public float Weight { get; set; }
        public float GravityAffectedFactor { get; set; }

        // collision related
        public List<ScreenSector> CurrentSectors { get; } = new List<ScreenSector>();
        public Rectangle CurrentObjectRectangle { get { return PositionRectangle; } }
        public GravityHandler GravityHandler { get; set; }

        // sprite helper
        private SpriteSheetHandler spriteSheet;

        public Player(GravityHandler gravityHandler)
        {
            GravityHandler = gravityHandler;
        }

        // public methods
        public override void Initialize()
        {
            // set object state
            SetInitialState();
        }

        private void SetInitialState()
        {
            Active = true;
            Visible = true;
            LayerIndex = 1;

            X = 10;
            Y = 0;

            Height = 16;
            Width = 16;

            PositionRectangle = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Width, Height);

            Direction.Value = Direction.NOVALUE;
            Velocity = 0;
            MaximumVelocity = 4.5f;
            Acceleration = 0;

            Weight = 1;
            GravityAffectedFactor = 1;

            if (assetName == null)
            {
                // texture related
                assetName = "Sprites\\Game Boy GBC - Final Fantasy Adventure - Sumo";
                textureId = GraphicsUtility.LoadTexture(assetName);
                spriteSheet = new SpriteSheetHandler(textureId, 4, 0, 10, 13, Width, Height);
                GraphicsUtility.AddVisibleGameObjects(LayerIndex, this);
            }

            spriteSheet.CurrentFrame = 3;
        }

        public override Rectangle GetDrawingRectangle()
        {
            return spriteSheet.getCurrentFrameRectangle();
        }

        public void ProcessUserInput(KeyboardState kstate, GamePadState gstate)
        {
            if (kstate.IsKeyDown(Keys.D1))
                spriteSheet.CurrentFrame--;

            else if (kstate.IsKeyDown(Keys.D2))
                spriteSheet.CurrentFrame++;

            if (kstate.IsKeyDown(Keys.Right))
            {
                spriteSheet.CurrentFrame = 3;

                if (Acceleration == 0)
                {
                    Acceleration = 0.2f;
                    Direction.Value = Direction.EAST;
                }
                else
                {
                    if (Direction.Value == Direction.EAST && Velocity > 0)
                        Acceleration -= 0.2f;
                    else
                    {
                        Acceleration += 0.2f;
                        Direction.SteerTowardsValue(Direction.EAST);
                    }
                }
            }
            else if (kstate.IsKeyDown(Keys.Left))
            {
                spriteSheet.CurrentFrame = 1;

                if (Acceleration == 0)
                {
                    Acceleration = 0.2f;
                    Direction.Value = Direction.WEST;
                }
                else
                {
                    if (Direction.Value == Direction.EAST && Velocity > 0)
                        Acceleration -= 0.2f;
                    else
                    {
                        Acceleration += 0.2f;
                        Direction.SteerTowardsValue(Direction.WEST);
                    }
                }
            }
            else if (kstate.IsKeyDown(Keys.Space))
            {
                Direction.Value = Direction.NORTH;
                if (Acceleration == 0)
                    Acceleration = 1f;
                GravityHandler.AddGravityAffectedObject(this);
            }
        }

        public void Move()
        {
            // change speed
            Velocity += Acceleration;
            Acceleration = 0;

            // apply friction         

            // Normalize values
            if (Velocity > MaximumVelocity)
                Velocity = MaximumVelocity;
            if (Velocity < 0)
                Velocity = 0;

            // Move
            float XVelocityModifier = 0;
            float YVelocityModifier = 0;

            if (Direction.Value > 270)
            {
                YVelocityModifier = 360 - Direction.Value;
                XVelocityModifier = 90 - YVelocityModifier;
            }
            else if (Direction.Value > 180)
            {
                XVelocityModifier = 270 - Direction.Value;
                YVelocityModifier = (90 - XVelocityModifier);
                XVelocityModifier *= -1;
            }
            else if (Direction.Value > 90)
            {
                YVelocityModifier = 180 - Direction.Value;
                XVelocityModifier = (90 - YVelocityModifier) * -1;
                YVelocityModifier *= -1;
            }
            else
            {
                YVelocityModifier = Direction.Value;
                XVelocityModifier = 90 - YVelocityModifier;
                YVelocityModifier *= -1;
            }
            XVelocityModifier = (XVelocityModifier / 90) * Velocity;
            YVelocityModifier = (YVelocityModifier / 90) * Velocity;

            X += XVelocityModifier;        
            Y += YVelocityModifier;
        }

        public override void Update()
        {
            // process input
            ProcessUserInput(InputHandler.CurrentKState, InputHandler.CurrentC1State);

            // move object
            Move();
        }

        public void HandleMobileCollision<T>(ICollidable collidedObject, CollisionDirection direction, params object[] extraParameters)
        {
            // surface handling
            if (typeof(T) == typeof(ISurface))
            {
                int moverBottomPoint = (int)extraParameters[0];
                int colliderBottomPoint = (int)extraParameters[1];

                // Adjust position
                // top collision
                if (direction == CollisionDirection.TOP)
                {
                    Y = Y - (moverBottomPoint - collidedObject.CurrentObjectRectangle.Y);

                    // Landed on a surface
                    Stopped = true;
                    GravityHandler.RemoveGravityAffectedObject(this);

                }
                if (direction == CollisionDirection.BOTTOM)
                    Y = colliderBottomPoint;
            }
        }
    }
}
