using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class Cup : FluidContainer
    {
        int size = 0;
        public Cup(int size)
        {
            this.size = size;
        }
        
        public int FillCup(int amount)
        {
            fluidLevel = Math.Clamp(amount - (size-fluidLevel),0,size);
            return amount - fluidLevel;
        }

    }
}
