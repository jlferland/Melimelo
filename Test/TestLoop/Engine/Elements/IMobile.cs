using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        void Move();
    }
}
