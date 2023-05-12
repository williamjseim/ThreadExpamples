namespace WebRequest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpAdventure adventure = new HttpAdventure();
            string s = Console.ReadLine();
            Console.WriteLine(adventure.GetResponse(s));
        }
    }
}