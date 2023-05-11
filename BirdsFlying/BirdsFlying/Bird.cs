using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdsFlying
{
    internal class Bird
    {
        public virtual void Drink()
        {
            Console.WriteLine("bird drinks");
        }

        public virtual void Eat()
        {
            Console.WriteLine("bird eats");
        }
    }
}
