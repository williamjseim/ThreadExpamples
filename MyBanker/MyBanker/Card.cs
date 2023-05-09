using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    internal abstract class Card
    {
        public string name { get; set; } = string.Empty;
        public string number { get; set; } = string.Empty;
        public Random random = new Random();
        public Card()
        {
            this.name = "søren hansen";
            this.number = CreateNumber();
        }

        protected virtual string CreateNumber()
        {
            return string.Empty;
        }

    }
}
