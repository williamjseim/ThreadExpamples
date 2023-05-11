namespace BirdsFlying
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Penguin penguin = new Penguin();
            penguin.Swim();
            Bird bird = new Red();
            bird.Drink();
            bird.Eat();
        }
    }
}