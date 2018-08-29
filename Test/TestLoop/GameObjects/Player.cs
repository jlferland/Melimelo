using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class Player : GameObject, ICollidable, IMobile, IUserInputtable
    {
        private List<ScreenSector> currentSectors = new List<ScreenSector>();
        private Rectangle currentObjectRectangle = new Rectangle();
        

        public Direction Direction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Stopped => throw new NotImplementedException();

        public float Weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float GravityAffectedFactor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<ScreenSector> CurrentSectors
        {
            get
            {
                return currentSectors;
            }
        }

        public Rectangle CurrentObjectRectangle
        {
            get
            {
                return currentObjectRectangle;
            }
        }

        public void ProcessUserInput()
        {
            throw new NotImplementedException();
        }
    }
}
