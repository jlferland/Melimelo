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
            GravityConstant = 9.8f; 
            GravityDirection.Value = Direction.SOUTH;
        }

        public void AddGravityAffectedObject(IMobile obj)
        {
            if (!GravityAffectedObjects.Contains(obj))
            {
                GravityAffectedObjects.Add(obj);

                objectStartTimeFalling.Add(obj, TimeSpan.Zero);
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
                if (objectStartTimeFalling[mobileObj] == TimeSpan.Zero)
                    objectStartTimeFalling[mobileObj] = EventClock.CurrentGameTime.TotalGameTime;

                float elapsedTime = (float)EventClock.CurrentGameTime.TotalGameTime.Subtract(objectStartTimeFalling[mobileObj]).TotalSeconds;
               
                mobileObj.Direction.SteerTowardsValue(GravityDirection.Value);

                if (mobileObj.Direction.Value - GravityDirection.Value > 90 || mobileObj.Direction.Value - GravityDirection.Value < -90)
                {
                    mobileObj.Acceleration -= (0.5f *
                           GravityConstant *
                           mobileObj.GravityAffectedFactor *
                           elapsedTime *
                           elapsedTime);

                    if (mobileObj.Velocity == 0)
                        mobileObj.Direction.Value = GravityDirection.Value;
                }
                else
                {
                    mobileObj.Acceleration += (0.5f *
                           GravityConstant *
                           mobileObj.GravityAffectedFactor *
                           elapsedTime *
                           elapsedTime);
                }

            }
        }
    }

}
