using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class TestGameArea : GameArea
    {
        private GravityEngine gravity;
        private Player player;

        public override void Initialize()
        {
            gravity = new GravityEngine();
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
