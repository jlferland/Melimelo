using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public class ScreenSector
    {
        public int Id { get; set; }
        public List<GameObject> ObjectsInSector { get; } = new List<GameObject>();
        public bool Visible; 

        public ScreenSector(int ID)
        {
            Id = ID;
        }
    }
}
