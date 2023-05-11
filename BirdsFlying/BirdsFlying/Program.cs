namespace BirdsFlying
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int y = 0; y < 40; y++)
            {
                for (int x = 0; x < 120; x++)
                {
                    if(y == 20)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                        Console.SetCursorPosition(x, y);
                        Console.Write(" ");
                }
            }
            Red bird = new Red();
            bird.SetLocation(50, 10);
            bird.Fly();
            while (true) { }

        }
    }
}