namespace KaffeMaskine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CoffeeMachine CoffeeMachine = new CoffeeMachine();
            CoffeeMachine.ConnectElectricity(true);
            CoffeeMachine.ChangeFilter(new CoffeeFilter(new CoffeeBean()));
            CoffeeMachine.FillWaterTank(76, FluidType.Water);
            Console.WriteLine(CoffeeMachine.TurnOn());
            CoffeeMachine teaMachine = new CoffeeMachine();
            teaMachine.ConnectElectricity(true);
            teaMachine.ChangeFilter(new TeaFilter(new Tea()));
            teaMachine.FillWaterTank(76, FluidType.Water);
            Console.WriteLine(teaMachine.TurnOn());
            CoffeeMachine.ChangeFilter(new CoffeeFilter(new Espresso()));
            teaMachine.FillWaterTank(100, FluidType.Water);
            teaMachine.ChangeCoffeeCan(new Cup(5));
            Console.WriteLine(teaMachine.MakeOneCup());
        }
    }
}