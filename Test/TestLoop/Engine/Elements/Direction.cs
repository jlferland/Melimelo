using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class Direction
    {
        // direction constants, in degrees
        public const short NORTH = 90;
        public const short SOUTH = 270;
        public const short EAST = 360;
        public const short WEST = 180;

        public bool Origin { get; set; } = true;

        private int value;
        public int Value
        {
            get { return value; }
            set
            {
                // between -360 and 360
                if (value > 360 || value < -360)
                    this.value = value % 360;
                else
                    this.value = value;

                // negative angles
                if (this.value < 0)
                    this.value = 360 + this.value;

                // if 0, make it 360
                if (this.value == 0)
                    this.value = EAST;

                Origin = false;
            }
        }

        public int SteerTowardsValue(int value)
        {
            int retrDifference = 0;

            if (Origin)
            {
                Value = value;
                Origin = false;
            }
            else
            {
                int tempValue = 0;
                if (value > 360 || value < 0)
                    tempValue = value % 360;
                else
                    tempValue = value;

                if (tempValue == 0)
                    tempValue = EAST;

                if (tempValue != Value)
                {
                    retrDifference = tempValue - Value;
                    while (retrDifference < -180) retrDifference += 360;
                    while (retrDifference > 180) retrDifference -= 360;
                    if (retrDifference > 90)
                    {
                        retrDifference = (180 - retrDifference);
                    }
                    else if (retrDifference < -90)
                    {
                        retrDifference = (180 + retrDifference);
                    }
                    float diff = retrDifference / 10f; // TODO : Isolate this 10 somewhere.

                    if (diff < 1 && diff > 0)
                        diff = 1;
                    else if (diff < 0 && diff > -1)
                         diff = -1;

                    Value += (int)diff; 
                }
            }

            return retrDifference;
        }
    }
}
