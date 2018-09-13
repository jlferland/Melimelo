using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class CVector2
    {
        public float X;
        public float Y;

        public Vector2 GetVector2()
        {
            return new Vector2(X, Y);
        }

        public Point GetPoint()
        {
            return new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
        }
    }
}
