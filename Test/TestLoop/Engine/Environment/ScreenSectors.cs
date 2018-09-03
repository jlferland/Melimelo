using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    /// <summary>
    /// matrix dividing game screen into sectors. Should facilitate collision detection.
    /// </summary>
    public class ScreenSectors
    {
        public enum SectorStyle
        {
            HorizontalDivision,
            VerticalDivision,
            Matrix
        }

        public List<ScreenSector> Sectors { get; set; }

        public ScreenSectors(GameArea areaToSectorize, int SectorSize, SectorStyle style)
        {

        }

        public void AssignSector(GameObject subject)
        {

        }
    }
}
