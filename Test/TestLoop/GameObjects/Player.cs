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
        public float Acceleration { get; set; }
        public bool Stopped { get => Velocity == 0; }

        // gravity related
        public float Weight { get; set; }
        public float GravityAffectedFactor { get; set; }

        // collision related
        public List<ScreenSector> CurrentSectors { get; } = new List<ScreenSector>();
        public Rectangle CurrentObjectRectangle { get { return PositionRectangle; } }

        // sprite helper
        private SpriteSheetHandler spriteSheet;

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

            ScreenPosition.X = -3;
            ScreenPosition.Y = 0;

            Height = 16;
            Width = 16;

            PositionRectangle = new Rectangle(Convert.ToInt32(ScreenPosition.X), Convert.ToInt32(ScreenPosition.Y), Width, Height);
            
            Direction.Value = Direction.EAST;
            Velocity = 0;
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
                spriteSheet.CurrentFrame++;
            else if (kstate.IsKeyDown(Keys.D2))
                spriteSheet.CurrentFrame--;
        }

        public void Move()
        {
            // change speed
            Velocity += Acceleration;
            Acceleration = 0;
             
            // apply friction            
            if (Direction.Value == Direction.SOUTH)
            {
                ScreenPosition.Y += Velocity;
                PositionRectangle.Y = Convert.ToInt32(ScreenPosition.Y);
            }
        }

        public override void Update()
        {
            // process input
            ProcessUserInput(InputHandler.CurrentKState, InputHandler.CurrentC1State);

            // move object
            Move();
        }

    }
}
