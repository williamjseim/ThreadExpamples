using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class TeaFilter : Filter
    {
        public TeaFilter(Tea tea)
        {
            Product = tea;
        }

        public override FluidType RunFluidThrough(FluidType fluidType)
        {
            if (Product.fluidType == FluidType.Null) { return fluidType; }
            return Product.fluidType;
        }
    }
}
