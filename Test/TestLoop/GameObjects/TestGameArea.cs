using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class TestGameArea : GameArea
    {
        private Player player;
        private Tile tile, tile2, tile3;

        public TestGameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight) : 
            base(screenPositionX, screenPositionY, viewportWidth, viewportHeight) 
        {

        }

        public override void Initialize()
        {
            // change parameters before calling base.initialize
            PixelsPerMeters = 1f;
            MaxFramePerSecond = 60;

            // initialize parent obj
            base.Initialize();

            // initialize custom items
            player = new Player(this);
            tile = new Tile(this, 10, 100, 3, 2);
            tile2 = new Tile(this, 100, 80, 3, 2);
            tile3 = new Tile(this, 80, 260, 6, 2);

            // register new objects to appropriate handlers
            AddGameObject(player, tile, tile2, tile3);
        }
    }
}
