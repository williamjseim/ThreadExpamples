using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class CoffeeFilter : Filter
    {
        public Product beans { get; set; }

        public CoffeeFilter(Product beans)
        {
            this.beans = beans;
        }

        public override FluidType RunFluidThrough(FluidType fluidType)
        {
            if(beans.fluidType == FluidType.Null) { return fluidType; }
            return beans.fluidType;
        }

    }
}
