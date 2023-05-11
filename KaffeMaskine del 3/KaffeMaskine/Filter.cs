using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class Filter
    {
        protected Product? Product;//skiftede det her behøvede ikke men det var pænere
        public virtual FluidType RunFluidThrough(FluidType fluidType)
        {
            return fluidType;
        }
    }
}