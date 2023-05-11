using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class CoffeeBean : Product
    {
        public override FluidType fluidType => FluidType.Coffee;
    }
}
