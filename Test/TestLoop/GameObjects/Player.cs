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
        public enum PlayerState
        {
            STOPPED = 0,
            WALKING = 1,
            RUNNING = 2,
            JUMPING = 4,
            FALLING = 8
        }

        // movement related
        public PlayerState State { get; set; }
        public Direction Direction { get; } = new Direction();
        public float Velocity { get; set; }
        public float MaximumVelocity { get; set; }
        public float Acceleration { get; set; }
        public bool Stopped
        {
            get => Velocity == 0;
            set { Velocity = 0; Acceleration = 0; Direction.Origin = true; State = PlayerState.STOPPED; }
        }

        // movement custom to this object (m/s)
        public float WalkingSpeed { get; set; } = 1f;
        public float RunningSpeed { get; set; } = 2f;

        // gravity related
        public float Weight { get; set; }
        public float GravityAffectedFactor { get; set; }

        // collision related
        public List<ScreenSector> CurrentSectors { get; } = new List<ScreenSector>();
        public Rectangle CurrentObjectRectangle { get { return PositionRectangle; } }

        // sprite helper
        private SpriteSheetHandler spriteSheet;

        // currently landed surface
        private GameObject surface;

        public Player(GameArea currentGameArea) : base(currentGameArea)
        {
        }

        // public methods
        public override void Initialize()
        {
            // set object state
            SetInitialState();
        }

        private void SetInitialState()
        {
            State = PlayerState.FALLING;

            Active = true;
            Visible = true;
            LayerIndex = 1;

            X = 10;
            Y = 0;

            Height = 16;
            Width = 16;

            PositionRectangle = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Width, Height);

            Direction.Origin = true;
            Velocity = 0;
            MaximumVelocity = 5f;
            Acceleration = 0;

            Weight = 1;
            GravityAffectedFactor = 1;
            CurrentGameArea.Gravity.AddGravityAffectedObject(this);
            CurrentGameArea.Collision.AddCollidableObject(this);

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

            if (kstate.IsKeyDown(Keys.LeftAlt))
                SetInitialState();

            if (kstate.IsKeyDown(Keys.Right))
            {
                spriteSheet.CurrentFrame = 3;

                if (Velocity <= 0)
                {
                    Acceleration = 0.2f;
                    Direction.Value = Direction.EAST;
                }
                else
                {
                    if (Direction.Value == Direction.WEST && Velocity > 0)
                        Velocity -= 0.2f;
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

                if (Velocity <= 0)
                {
                    Acceleration = 0.2f;
                    Direction.Value = Direction.WEST;
                }
                else
                {
                    if (Direction.Value == Direction.EAST && Velocity > 0)
                        Velocity -= 0.2f;
                    else
                    {
                        Acceleration += 0.2f;
                        Direction.SteerTowardsValue(Direction.WEST);

                    }
                }
            }


            if (kstate.IsKeyDown(Keys.Space))
            {
                if (State != PlayerState.FALLING)
                {

                    if (Velocity <= 0)
                    {
                        CurrentGameArea.Gravity.AddGravityAffectedObject(this);
                        Direction.Value = Direction.NORTH;
                        Acceleration = 2f;
                    }
                    else
                    {
                        if (State == PlayerState.JUMPING)
                        {
                            Direction.SteerTowardsValue(Direction.NORTH);
                        }
                        else
                        {
                            CurrentGameArea.Gravity.AddGravityAffectedObject(this);
                            Direction.Value = Direction.NORTH;
                        }

                        Acceleration += 1f;
                    }

                    State = PlayerState.JUMPING;
                }
            }
        }

        public void Move(GameTime gameTime)
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

            // check for surface
            if (surface != null)
            {
                // y axis
                if (this.PositionRectangle.Bottom < surface.PositionRectangle.Y)
                {
                    State = PlayerState.FALLING;
                    CurrentGameArea.Gravity.AddGravityAffectedObject(this);
                }
                else if (this.PositionRectangle.X > surface.PositionRectangle.Right || this.PositionRectangle.Right < surface.PositionRectangle.X)
                {
                    State = PlayerState.FALLING;
                    CurrentGameArea.Gravity.AddGravityAffectedObject(this);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // process input
            ProcessUserInput(InputHandler.CurrentKState, InputHandler.CurrentC1State);

            // move object
            Move(gameTime);
        }

        public void HandleMobileCollision<T>(ICollidable collidedObject, CollisionDirection direction)
        {
            // surface handling
            if (typeof(T) == typeof(ISurface))
            {
                // Adjust position
                // top collision
                if (direction == CollisionDirection.TOP)
                {
                    Y = Y - (this.PositionRectangle.Bottom - collidedObject.CurrentObjectRectangle.Y);

                    // Landed on a surface
                    Stopped = true;

                    CurrentGameArea.Gravity.RemoveGravityAffectedObject(this);
                    surface = (GameObject)collidedObject;
                }
                if (direction == CollisionDirection.BOTTOM)
                {
                    // stopped by a surface
                    Stopped = true;
                    Y = ((GameObject)collidedObject).PositionRectangle.Bottom;
                }
            }
        }
    }
}
