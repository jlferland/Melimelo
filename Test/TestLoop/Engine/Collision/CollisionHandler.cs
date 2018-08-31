using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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

            // object collision
            foreach (ICollidable objMover in collidableObjects)
            {
                // first object must be moving
                if (typeof(IMobile).IsAssignableFrom(objMover.GetType()))
                {
                    IMobile mover = (IMobile)objMover;

                    if (!mover.Stopped)
                    {
                        // scan all other objects to see if there is a collision with them
                        foreach (ICollidable objCollider in collidableObjects)
                        {
                            if (objMover != objCollider && 
                                objCollider.CurrentObjectRectangle.Intersects(objMover.CurrentObjectRectangle))
                            {
                                // detect collision direction
                                CollisionDirection direction = CollisionDirection.NONE;
                                int moverBottomPoint = objMover.CurrentObjectRectangle.Y + objMover.CurrentObjectRectangle.Height;
                                int colliderBottomPoint = objCollider.CurrentObjectRectangle.Y + objCollider.CurrentObjectRectangle.Height;

                                if (moverBottomPoint > objCollider.CurrentObjectRectangle.Y && moverBottomPoint < colliderBottomPoint)
                                {
                                    direction = CollisionDirection.TOP;
                                }
                                else if (colliderBottomPoint > objMover.CurrentObjectRectangle.Y)
                                {
                                    direction = CollisionDirection.BOTTOM;
                                }

                                // handle collision on the mover
                                if (typeof(ISurface).IsAssignableFrom(objCollider.GetType()))
                                {
                                    mover.HandleMobileCollision<ISurface>(objCollider, direction, moverBottomPoint, colliderBottomPoint);
                                }
                            }
                        }
                    }
                }
            }

            // game area edge collision


            // TODO : defer off screen collision to another Thread
        }

        private void RefreshSector(ICollidable collidableObject)
        {
            GameObject gameObj = (GameObject)collidableObject;



        }


    }
}
