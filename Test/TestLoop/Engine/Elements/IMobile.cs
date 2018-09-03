using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public interface IMobile
    {
        Direction Direction // degrees
        {
            get;
        }

        float Velocity
        {
            get;
            set;
        }

        float MaximumVelocity
        {
            get;
            set;
        }

        float Acceleration
        {
            get;
            set;
        }

        bool Stopped
        {
            get;
            set;
        }

        float Weight
        {
            get;
            set;
        }

        float GravityAffectedFactor // 1 = full effect, 0 = no effect
        {
            get;
            set;
        }

        void Move(GameTime gameTime);

        void HandleMobileCollision<T>(ICollidable collidedObject, CollisionDirection direction);
    }
}
