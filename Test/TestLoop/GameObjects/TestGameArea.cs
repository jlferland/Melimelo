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
        private Tile tile;

        public TestGameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight) : 
            base(screenPositionX, screenPositionY, viewportWidth, viewportHeight) 
        {

        }

        public override void Initialize()
        {
            // change parameters before calling base.initialize
            PixelsPerMeters = 10f;

            // initialize parent obj
            base.Initialize();

            // initialize custom items
            player = new Player(this);
            tile = new Tile(this);

            // register new objects to appropriate handlers
            AddGameObject(player, tile);
            Gravity.AddGravityAffectedObject(player);
            Collision.AddCollidableObject(player, tile);
        }

        public override void Update()
        {
            // moving objects
            base.Update();
        }
    }
}
