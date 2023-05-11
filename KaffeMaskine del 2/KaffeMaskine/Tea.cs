using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class Tea : Product
    {
        public override FluidType fluidType => FluidType.Tea;
    }
}
