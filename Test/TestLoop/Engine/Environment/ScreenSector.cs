using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class ScreenSector
    {
        public int Id;
        public List<GameObject> ObjectsInSector;

        public ScreenSector(int ID)
        {
            Id = ID;
            ObjectsInSector = new List<GameObject>();
        }
    }
}
