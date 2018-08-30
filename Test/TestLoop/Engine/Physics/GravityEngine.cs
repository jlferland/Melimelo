using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class GravityEngine
    {
        private List<IMobile> gravityAffectedObjects = new List<IMobile>();

        public float pullForce;
        public Direction gravityDirection { get; } = new Direction();

        public GravityEngine()
        {
            pullForce = 2;
            gravityDirection.Value = Direction.SOUTH;
        }

        public void AddGravityAffectedObject(IMobile obj)
        {
            if (!gravityAffectedObjects.Contains(obj))
                gravityAffectedObjects.Add(obj);
        }

        public void Apply()
        {
            foreach (IMobile mobileObj in gravityAffectedObjects)
            {
                mobileObj.Acceleration += (pullForce * mobileObj.GravityAffectedFactor);
                mobileObj.Direction.Value = gravityDirection.Value;
            }
        }
    }

}
