namespace KaffeMaskine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CoffeeMachine machine = new CoffeeMachine();
            machine.ConnectElectricity(true);
            machine.ChangeFilter(new CoffeeFilter(new CoffeeBean()));
            machine.FillWaterTank(76, FluidType.Water);
            Console.WriteLine(machine.TurnOn());
        }
    }
}