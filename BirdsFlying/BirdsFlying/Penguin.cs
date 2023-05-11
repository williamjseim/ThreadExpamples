using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdsFlying
{
    internal class Penguin : Bird, ISwim
    {
        public void Swim()
        {
            Console.WriteLine("bird swims");
        }
    }
}
