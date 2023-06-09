﻿using System;
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

        public override FluidType RunFluidThrough(FluidType fluidType)//kunne godt flytte op i filter og fjerne teafilter og coffeefilter men
        {
            if (Product.fluidType == FluidType.Null) { return fluidType; }
            return Product.fluidType;
        }
    }
}
