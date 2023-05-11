using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class CoffeeMachine : KitchenApplience
    {
        protected bool heaterPlate = false;
        protected Filter? filter;
        protected FluidContainer WaterTank;
        protected FluidContainer CoffeeCan;

        public CoffeeMachine()
        {
            WaterTank = new FluidContainer();
            CoffeeCan = new FluidContainer();
        }

        public void ChangeFilter(Filter filter)
        {
            this.filter = filter;
        }

        public void ChangeCoffeeCan(FluidContainer newCoffeeCan)
        {
            CoffeeCan = newCoffeeCan;
        }

        public string TurnOn()
        {
            On = true;
            if(On && filter != null)
            {
                if(WaterTank.fluidLevel > 0)
                {
                    CoffeeCan.Type = filter.RunFluidThrough(WaterTank.Type);
                    CoffeeCan.fluidLevel = WaterTank.Empty();
                    return $"making {CoffeeCan.fluidLevel} {CoffeeCan.Type}";
                }
                return "no water";
            }
            return "no electricity, filter or water";
        }
        
        public string MakeOneCup()
        {
            On = true;
            if(On && filter != null)
            {
                if(WaterTank.fluidLevel > 0)
                {
                    if(CoffeeCan.GetType() == typeof(Cup))
                    {
                        CoffeeCan.Type = filter.RunFluidThrough(WaterTank.Type);
                        WaterTank.fluidLevel = ((Cup)CoffeeCan).FillCup(WaterTank.fluidLevel);
                        return $"Made one cup of {CoffeeCan.Type} with {CoffeeCan.fluidLevel}";
                    }
                    return "Put a cup in the coffee machine";
                }
                return "No water";
            }
            return "No electricity or filter";
        }

        public void FillWaterTank(int waterLevel, FluidType fluidType)
        {
            WaterTank.FillContainer(waterLevel, fluidType);
        }
    }
}
