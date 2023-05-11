using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class Product
    {
        public virtual FluidType fluidType { get; private set; } = FluidType.Null;
    }
}
