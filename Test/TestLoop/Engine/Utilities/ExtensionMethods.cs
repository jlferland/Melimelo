using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public static class ExtensionMethods
    {
        public static Point Origin(this Vector2 v)
        {
            return new Point(Convert.ToInt32(v.X), Convert.ToInt32(v.Y));
        }
    }
}
