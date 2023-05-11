using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    public enum FluidType
    {
        Null,
        Water,
        Coffee,
        Tea,
    }
    internal class FluidContainer
    {
        public FluidType Type { get; set; } = FluidType.Null;
        public int fluidLevel = 0;

        public void FillContainer(int fluidAmount, FluidType fluidType)
        {
            this.fluidLevel += fluidAmount;
            this.Type = fluidType;
            Math.Clamp(fluidLevel, 0, 100);
        }

        public int Empty()
        {
            int i = fluidLevel;
            fluidLevel = 0;
            return i;
        }

    }
}
