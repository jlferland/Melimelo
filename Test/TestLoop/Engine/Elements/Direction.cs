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
        public const short NOVALUE = 0;

        private int value;
        public int Value
        {
            get { return value; }
            set
            {
                if (value > 360 || value < 0)
                    this.value = value % 360;
                else
                    this.value = value;
            }
        }

        public void SteerTowardsValue(int value)
        {
            if (Value == NOVALUE)
            {
                Value = value;
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
                    int difference = tempValue - Value;
                    while (difference < -180) difference += 360;
                    while (difference > 180) difference -= 360;
                    if (difference > 90)
                    {
                        difference = (180 - difference);
                    }
                    else if (difference < -90)
                    {
                        difference = (180 + difference);
                    }
                    int diff = difference / 10;
                    if (diff < 1 && diff > 0)
                        diff = 1;
                    else if (diff < 0 && diff > -1)
                        diff = -1;

                    Value += diff; // TODO : Isolate this 10 somewhere.
                }
            }
        }
    }
}
