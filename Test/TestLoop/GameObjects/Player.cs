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
        public CVector2 Velocity { get; } = new CVector2();
        public CVector2 MaximumVelocity { get; } = new CVector2();
        public CVector2 NegativeMaximumVelocity { get; } = new CVector2();
        public CVector2 Acceleration { get; } = new CVector2();

        public bool Stopped
        {
            get => Velocity.X == 0 &&
                   Velocity.Y == 0 &&
                   State == PlayerState.STOPPED;
            set
            {
                Velocity.X = 0;
                Velocity.Y = 0;
                Acceleration.X = 0;
                Acceleration.Y = 0;
                State = PlayerState.STOPPED;
            }
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

            Velocity.X = 0;
            Velocity.Y = 0;
            MaximumVelocity.X = 6f;
            MaximumVelocity.Y = 10f;
            NegativeMaximumVelocity.X = -6f;
            NegativeMaximumVelocity.Y = -10f;
            Acceleration.X = 0;
            Acceleration.Y = 0;

            PositionRectangle = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Width, Height);
            
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

            else if (kstate.IsKeyDown(Keys.OemMinus))
                CurrentGameArea.Scale += 0.1f;
            else if (kstate.IsKeyDown(Keys.OemPlus))
                CurrentGameArea.Scale -= 0.1f;
            

            if (kstate.IsKeyDown(Keys.LeftAlt))
                SetInitialState();

            if (kstate.IsKeyDown(Keys.Right))
            {
                spriteSheet.CurrentFrame = 3;
                Acceleration.X += 0.2f;
            }
            else if (kstate.IsKeyDown(Keys.Left))
            {
                spriteSheet.CurrentFrame = 1;
                Acceleration.X -= 0.2f;
            }
            
            if (kstate.IsKeyDown(Keys.Space))
            {
                if (State != PlayerState.FALLING)
                {
                    if (State != PlayerState.JUMPING)
                    {
                        CurrentGameArea.Gravity.AddGravityAffectedObject(this);
                        State = PlayerState.JUMPING;
                    }
                    Acceleration.Y -= MaximumVelocity.Y;
                }
            }            
        }

        public void Move(GameTime gameTime)
        {
            // change speed
            Velocity.X += Acceleration.X;
            Velocity.Y += Acceleration.Y;
            Acceleration.X = 0;
            Acceleration.Y = 0;

            // apply friction         

            // Normalize values
            if (Velocity.X > MaximumVelocity.X)
                Velocity.X = MaximumVelocity.X;
            if (Velocity.X < NegativeMaximumVelocity.X)
                Velocity.X = NegativeMaximumVelocity.X;
            if (Velocity.Y > MaximumVelocity.Y)
                Velocity.Y = MaximumVelocity.Y;
            if (Velocity.Y < NegativeMaximumVelocity.Y)
                Velocity.Y = NegativeMaximumVelocity.Y;

            // Move
            X += Velocity.X * CurrentGameArea.PixelsPerMeters;
            Y += Velocity.Y * CurrentGameArea.PixelsPerMeters;

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
                    State = PlayerState.FALLING;

                    Y = ((GameObject)collidedObject).PositionRectangle.Bottom;
                }
            }
        }

        public override void DeScale()
        {
            base.DeScale();
            MaximumVelocity.X /=  CurrentGameArea.Scale;
            MaximumVelocity.Y /=  CurrentGameArea.Scale;
            NegativeMaximumVelocity.X /= CurrentGameArea.Scale;
            NegativeMaximumVelocity.Y /= CurrentGameArea.Scale;
        }

        public override void ReScale()
        {
            base.ReScale();
            MaximumVelocity.X *= CurrentGameArea.Scale;
            MaximumVelocity.Y *= CurrentGameArea.Scale;
            NegativeMaximumVelocity.X *= CurrentGameArea.Scale;
            NegativeMaximumVelocity.Y *= CurrentGameArea.Scale;
        }
    }
}
