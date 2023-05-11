using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdsFlying
{
    internal class Penguin : Bird, ISwim
    {
        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void SetAltitude(double altitude)
        {
            throw new NotImplementedException();
        }

        public override void SetLocation(double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            Console.WriteLine("bird swims");
        }
    }
}
