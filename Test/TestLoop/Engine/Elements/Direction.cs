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
        public const short EAST = 0;
        public const short WEST = 180;

        private int value;
        public int Value
        {
            get { return value; }
            set {
                if (value > 360 || value < 0)
                    this.value = value % 360;
                else
                    this.value = value;
                }
        }
    }
}
