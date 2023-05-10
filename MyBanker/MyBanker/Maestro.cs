using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    internal class Maestro : ExpirableCard
    {
        public Maestro() : base()
        {
            
        }

        string prefixs = "5018,5020,5038,5893,6304,6759,6761,6762,6763";//holds all the prefix number that the card can start with
        protected override string CreateNumber()
        {
            string[] prefixes = prefixs.Split(',');//splits the prefixs numbers into an array
            string number = prefixes[random.Next(0, prefixes.Length)];//choses a random prefix number
            int j = number.Length;//gets the length of the prefix
            for (int i = 0; i < 20 - j; i++)
            {
                number += random.Next(0, 10);//adds a random number to the list
            }
            return number;
        }

        protected override DateOnly? CreateExpirationDate()
        {
            DateOnly date = new DateOnly(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            date.AddYears(5);
            date.AddMonths(8);
            return date;
        }
    }
}
