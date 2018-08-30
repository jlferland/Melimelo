using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public interface ICollidable
    {
        List<ScreenSector> CurrentSectors
        {
            get;
        }

        Rectangle CurrentObjectRectangle
        {
            get;
        }

        // TODO collision map


    }
}
