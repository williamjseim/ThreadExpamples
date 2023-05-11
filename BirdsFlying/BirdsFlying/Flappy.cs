using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdsFlying
{
    internal class Flappy : Bird, IFly
    {
        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public void Fly()
        {
            Console.WriteLine("bird flys");
        }

        public override void SetLocation(double longitude, double latitude)
        {
            throw new NotImplementedException();
        }
    }
}
