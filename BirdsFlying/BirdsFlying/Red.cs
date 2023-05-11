using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdsFlying
{
    internal class Red : Bird, IFly
    {
        public void Fly()
        {
            Console.WriteLine("bird flys");
        }
    }
}
