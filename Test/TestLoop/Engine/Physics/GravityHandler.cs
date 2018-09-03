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

        public float GravitationalConstant { get; set; }
        public float AirDensity { get; set; }

        public Direction GravityDirection { get; } = new Direction();
        public List<IMobile> GravityAffectedObjects = new List<IMobile>();

        public GravityHandler(float gravitationalConstant, int gravityDirection, float airDensity)
        {
            GravitationalConstant = gravitationalConstant;
            GravityDirection.Value = gravityDirection;
            AirDensity = airDensity;
        }

        public void AddGravityAffectedObject(params IMobile[] objects)
        {
            foreach (IMobile obj in objects)
            {
                if (!GravityAffectedObjects.Contains(obj))
                {
                    GravityAffectedObjects.Add(obj);

                    objectStartTimeFalling.Add(obj, TimeSpan.Zero);
                }
            }
        }

        public void RemoveGravityAffectedObject(params IMobile[] objects)
        {
            foreach (IMobile obj in objects)
            {
                if (GravityAffectedObjects.Contains(obj))
                {
                    GravityAffectedObjects.Remove(obj);

                    if (objectStartTimeFalling.ContainsKey(obj))
                        objectStartTimeFalling.Remove(obj);
                }
            }
        }

        public void Apply(GameTime gameTime)
        {
            foreach (IMobile mobileObj in GravityAffectedObjects)
            {
                if (objectStartTimeFalling[mobileObj] == TimeSpan.Zero)
                    objectStartTimeFalling[mobileObj] = gameTime.TotalGameTime;

                float elapsedTime = (float)gameTime.TotalGameTime.Subtract(objectStartTimeFalling[mobileObj]).TotalSeconds;

                mobileObj.Direction.SteerTowardsValue(GravityDirection.Value);

                if (mobileObj.Direction.Value - GravityDirection.Value > 90 || mobileObj.Direction.Value - GravityDirection.Value < -90)
                {
                    mobileObj.Acceleration -= (AirDensity *
                           GravitationalConstant *
                           mobileObj.GravityAffectedFactor *
                           elapsedTime *
                           elapsedTime);

                    if (mobileObj.Velocity == 0)
                        mobileObj.Direction.Value = GravityDirection.Value;
                }
                else
                {
                    mobileObj.Acceleration += (AirDensity *
                           GravitationalConstant *
                           mobileObj.GravityAffectedFactor *
                           elapsedTime *
                           elapsedTime);
                }

            }
        }
    }

}
