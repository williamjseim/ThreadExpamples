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

        string prefixs = "5018,5020,5038,5893,6304,6759,6761,6762,6763";
        protected override string CreateNumber()
        {
            string[] prefixes = prefixs.Split(',');
            string chosen = prefixes[random.Next(0, prefixes.Length)];
            int j = chosen.Length;
            for (int i = 0; i < 20 - j; i++)
            {
                chosen += random.Next(0, 10);
            }
            return chosen;
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
