using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class GravityHandler
    {
        private List<IMobile> gravityAffectedObjects = new List<IMobile>();
        private Dictionary<IMobile, TimeSpan> objectStartTimeFalling = new Dictionary<IMobile, TimeSpan>();

        public float gravityConstant;
        public Direction gravityDirection { get; } = new Direction();

        public GravityHandler()
        {
            gravityConstant = 0.49f; // Earth gravity scaled at .05
            gravityDirection.Value = Direction.SOUTH;
        }

        public void AddGravityAffectedObject(IMobile obj)
        {
            if (!gravityAffectedObjects.Contains(obj))
            {
                gravityAffectedObjects.Add(obj);

                if (EventClock.CurrentGameTime == null)
                    objectStartTimeFalling.Add(obj, TimeSpan.Zero);
                else
                    objectStartTimeFalling.Add(obj, EventClock.CurrentGameTime.TotalGameTime);
            }
        }

        public void Apply()
        {
            foreach (IMobile mobileObj in gravityAffectedObjects)
            {
                float elapsedTime = (float)EventClock.CurrentGameTime.TotalGameTime.Subtract(objectStartTimeFalling[mobileObj]).TotalSeconds; 

                mobileObj.Acceleration += (0.5f * 
                                           gravityConstant * 
                                           mobileObj.GravityAffectedFactor *
                                           elapsedTime *
                                           elapsedTime);

                mobileObj.Direction.Value = gravityDirection.Value;
            }
        }
    }

}
