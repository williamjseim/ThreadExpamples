using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdsFlying
{
    internal abstract class Bird
    {
        public VectorI2D Position;
        public abstract void SetLocation(double longitude, double latitude);
        public abstract void Draw();
    }

    public struct VectorI2D
    {
        public int x;
        public int y;
    }
}
