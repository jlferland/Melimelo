using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoop
{
    public interface ISurface
    {
        float Friction
        {
            get;
            set;
        }

        float Bounce
        {
            get;
            set;
        }
    }
}
