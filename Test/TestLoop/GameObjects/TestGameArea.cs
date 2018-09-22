using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class TestGameArea : GameArea
    {
        private Player player;
        private Tile tile, tile2, tile3, tile4;

        public TestGameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight) : 
            base(screenPositionX, screenPositionY, viewportWidth, viewportHeight) 
        {

        }

        public override void Initialize()
        {
            // change parameters before calling base.initialize
            PixelsPerMeters = 1f;
            MaxFramePerSecond = 60;
            EdgeBehaviorX = GameAreaEdgeBehavior.EDGE_SCROLL;

            // initialize parent obj
            base.Initialize();

            // initialize custom items
            player = new Player(this);
            tile = new Tile(this, 10, 100, 3, 2);
            tile2 = new Tile(this, 100, 80, 3, 2);
            tile3 = new Tile(this, 80, 260, 6, 2);
            tile4 = new Tile(this, 140, 200, 18, 2);

            // register new objects to appropriate handlers
            AddGameObject(player, tile, tile2, tile3, tile4);
        }

        public override void Update(GameTime gameTime)
        {
            // check if we need to pin or unpin the player
            if (player.PositionRectangle.X > this.ViewportRectangle.Width / 2)
            {
                PinViewportOnObject(player);
            }
            
            // base update
            base.Update(gameTime);
        }
    }
}
