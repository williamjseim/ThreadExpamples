namespace BirdsFlying
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 120; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
            Bird bird = new Red();
            bird.SetLocation(50, 10);
            bird.Draw();
            while (true) { }

        }
    }
}