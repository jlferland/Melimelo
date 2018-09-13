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
        CVector2 Velocity
        {
            get;
        }

        CVector2 MaximumVelocity
        {
            get;
        }

        CVector2 Acceleration
        {
            get;
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
