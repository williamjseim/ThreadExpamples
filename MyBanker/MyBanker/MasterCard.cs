using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    internal class MasterCard : ExpirableCard ,IMonthlyAllowence
    {
        public MasterCard() : base()
        {
            
        }

        string prefixs = "51,52,53,54,55";
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

        private int monthlyAllowence = 20000;
        public int MonthlyAllowence { get { return monthlyAllowence; } }
    }
}
