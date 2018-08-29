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
        public Rectangle CurrentObjectRectangle { get; } = new Rectangle();

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

            Position.X = 1;
            Position.Y = 1;

            Height = 16;
            Width = 16;

            Direction.Value = Direction.EAST;
            Velocity = 0;
            Acceleration = 0;

            Weight = 1;
            GravityAffectedFactor = 1;

            if (assetName == null)
            {
                // texture related
                assetName = "Sprites\\Game Boy GBC - Final Fantasy Adventure - Sumo";
                textureId = GraphicsUtility.LoadTexture(assetName, this);
                spriteSheet = new SpriteSheetHandler(this, 4, 0, 10, 13);
                GraphicsUtility.VisibleGameObjects.Add(this);
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
    }
}
