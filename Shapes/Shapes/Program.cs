namespace Shapes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Square square = new Square(4);
            Console.WriteLine(square.Size);
            Console.WriteLine(square.Area);
            Parallelogram parallelogram = new Parallelogram(3, 5, 20);
            Console.WriteLine(parallelogram.Area);
            Trapez t = new Trapez(10, 9, 8, 9);
            Console.WriteLine(t.Area);
            Triangle triangle = new Triangle(10, 9, 5);
            Console.WriteLine(triangle.Area+"adsasdwa");
        }
    }
}