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
        private Dictionary<IMobile, TimeSpan> objectStartTimeFalling = new Dictionary<IMobile, TimeSpan>();

        public float GravityConstant;
        public Direction GravityDirection { get; } = new Direction();
        public List<IMobile> GravityAffectedObjects = new List<IMobile>();

        public GravityHandler()
        {
            GravityConstant = 0.49f; // Earth gravity scaled at .05
            GravityDirection.Value = Direction.SOUTH;
        }

        public void AddGravityAffectedObject(IMobile obj)
        {
            if (!GravityAffectedObjects.Contains(obj))
            {
                GravityAffectedObjects.Add(obj);

                if (EventClock.CurrentGameTime == null)
                    objectStartTimeFalling.Add(obj, TimeSpan.Zero);
                else
                    objectStartTimeFalling.Add(obj, EventClock.CurrentGameTime.TotalGameTime);
            }
        }

        public void RemoveGravityAffectedObject(IMobile obj)
        {
            if (GravityAffectedObjects.Contains(obj))
            {
                GravityAffectedObjects.Remove(obj);

                if (objectStartTimeFalling.ContainsKey(obj))
                    objectStartTimeFalling.Remove(obj);
            }
        }

        public void Apply()
        {
            foreach (IMobile mobileObj in GravityAffectedObjects)
            {                
                float elapsedTime = (float)EventClock.CurrentGameTime.TotalGameTime.Subtract(objectStartTimeFalling[mobileObj]).TotalSeconds;

                mobileObj.Acceleration += (0.5f *
                                           GravityConstant *
                                           mobileObj.GravityAffectedFactor *
                                           elapsedTime *
                                           elapsedTime);

                mobileObj.Direction.SteerTowardsValue(GravityDirection.Value);
            }
        }
    }

}
