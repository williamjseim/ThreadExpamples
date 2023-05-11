using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class Espresso : Product
    {
        public override FluidType fluidType => FluidType.Espresso;
    }
}
