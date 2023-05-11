using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdsFlying
{
    internal class Flappy : Bird, IFly
    {
        public void Fly()
        {
            Console.WriteLine("bird flys");
        }
    }
}
