namespace WebRequest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DiskAventure adventure = new DiskAventure();
            string s = Console.ReadLine();
            Console.WriteLine(adventure.GetResponse(s));
        }
    }
}