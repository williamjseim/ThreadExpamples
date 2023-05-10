using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    internal class Visa : ExpirableCard ,IMonthlyAllowence
    {
        public Visa() : base()
        {
            
        }

        protected override string CreateNumber()
        {
            string chosen = "4";//first number
            int j = chosen.Length;
            for (int i = 0; i < 17-j; i++)//adds the rest of the numbers
            {
                chosen += random.Next(0, 10);
            }
            return chosen;
        }
        private int monthlyAllowence = 20000;
        public int MonthlyAllowence { get { return monthlyAllowence; } } 
    }
}
