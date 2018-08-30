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
        private Player player;

        public TestGameArea(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight) : 
            base(screenPositionX, screenPositionY, viewportWidth, viewportHeight) 
        {

        }

        public override void Initialize()
        {
            gravity = new GravityHandler();
            player = new Player();

            AddGameObject(player);
            AddGameObject(new Tile());

            gravity.AddGravityAffectedObject(player);
        }

        public override void Update()
        {
            gravity.Apply();

            base.Update();
        }
    }
}
