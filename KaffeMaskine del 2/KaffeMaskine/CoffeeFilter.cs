using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class CoffeeFilter : Filter
    {

        public CoffeeFilter(Product beans)
        {
            this.Product = beans;
        }

        public override FluidType RunFluidThrough(FluidType fluidType)
        {
            if(Product.fluidType == FluidType.Null) { return fluidType; }
            return Product.fluidType;
        }

    }
}
