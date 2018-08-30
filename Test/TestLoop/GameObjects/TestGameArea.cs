using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class TestGameArea : GameArea
    {
        private GravityHandler gravity;
        private CollisionHandler collision;

        private Player player;
        private Tile tile;

        public TestGameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight) : 
            base(screenPositionX, screenPositionY, viewportWidth, viewportHeight) 
        {

        }

        public override void Initialize()
        {
            gravity = new GravityHandler();
            collision = new CollisionHandler(this);

            player = new Player();
            tile = new Tile();

            AddGameObject(player);
            AddGameObject(tile);

            gravity.AddGravityAffectedObject(player);

            collision.AddCollidableObject(player);
            collision.AddCollidableObject(tile);            
        }

        public override void Update()
        {
            gravity.Apply();
            collision.HandleCollisions();
            base.Update();
        }
    }
}
