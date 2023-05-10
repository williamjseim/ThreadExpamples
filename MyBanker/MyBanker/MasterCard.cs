using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    internal class MasterCard : ExpirableCard ,IMonthlyAllowance
    {
        public MasterCard() : base()
        {
            
        }

        string prefixs = "51,52,53,54,55";//holds all the prefix number that the card can start with
        protected override string CreateNumber()//creates the card number 
        {
            string[] prefixes = prefixs.Split(',');//splits the prefixs numbers into an array
            string number = prefixes[random.Next(0, prefixes.Length)];//choses a random prefix number
            int j = number.Length;//gets the length of the prefix
            for (int i = 0; i < 17 - j; i++)
            {
                number += random.Next(0, 10);//adds a random number to the list
            }
            return number;
        }

        private int monthlyAllowence = 20000;
        public int MonthlyAllowence { get { return monthlyAllowence; } }
    }
}
