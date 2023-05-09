using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    internal class DebitCard : Card
    {
        public DebitCard() : base()
        {
            
        }

        protected override string CreateNumber()
        {
            string number = "2400";
            for (int i = 0; i < 17-number.Length; i++)
            {
                number += random.Next(0, 10);
            }
            return number;
        }

        public override string ToString()
        {
            return $"{number} \n {name}";
        }
    }
}
