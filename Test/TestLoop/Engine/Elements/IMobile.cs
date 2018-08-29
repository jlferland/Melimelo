using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    interface IMobile
    {
        /* 
        // movement related
        public short Direction = TestLoop.Direction.EAST; // 360 degrees
        public float Velocity = 0;
        public float Acceleration = 0;

        public bool Stopped
        {
            get { return Velocity == 0; }
        }

        // gravity related
        public float Weight;
        public short GravityAffectedPercentage = 0;
        */

        Direction Direction // degrees
        {
            get;
            set;
        }

        float Velocity
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
        }


        float Weight
        {
            get;
            set;
        }

        float GravityAffectedFactor // 1 = full effect
        {
            get;
            set;
        }

    }
}
