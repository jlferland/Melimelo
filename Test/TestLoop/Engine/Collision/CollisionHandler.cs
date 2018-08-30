using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class CollisionHandler
    {
        private List<ICollidable> collidableObjects = new List<ICollidable>();
        private ScreenSectors sectors;

        public CollisionHandler(GameArea currentArea)
        {
            sectors = new ScreenSectors(currentArea, 8, ScreenSectors.SectorStyle.Matrix);
        }

        public void AddCollidableObject(ICollidable obj)
        {
            if (!collidableObjects.Contains(obj))
            {
                collidableObjects.Add(obj);
                RefreshSector(obj);
            }
        }

        public void HandleCollisions()
        {
            // handle on screen collision

            // defer off screen collision to another Thread
        }

        private void RefreshSector(ICollidable collidableObject)
        {
            GameObject gameObj = (GameObject)collidableObject;

            

        }


    }
}
