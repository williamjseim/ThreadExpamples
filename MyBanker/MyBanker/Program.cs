namespace MyBanker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Visa visa = new Visa();
            Console.WriteLine(visa.ToString());
            MasterCard masterCard = new MasterCard();
            Console.WriteLine(masterCard.ToString());
        }
    }
}