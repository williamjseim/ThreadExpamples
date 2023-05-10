using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    internal class VisaElectron : ExpirableCard
    {
        public VisaElectron() : base()
        {
            
        }
        string prefixs = "4026,417500,4844,4913,4917";//holds all the prefix number that the card can start with

        protected override string CreateNumber()
        {
            string[] prefixes = prefixs.Split(',');
            string chosen = prefixes[random.Next(0, prefixes.Length)];
            int j = chosen.Length;
            for (int i = 0; i < 17 - j; i++)
            {
                chosen += random.Next(0, 10);
            }
            return chosen;
        }
    }
}
