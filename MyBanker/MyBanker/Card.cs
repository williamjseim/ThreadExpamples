using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBanker
{
    internal abstract class Card
    {
        protected string name { get; set; } = string.Empty;//card holder name
        protected string number { get; set; } = string.Empty;//card number
        public Random random = new Random();//random here so i only had to put one on the class diagram
        public Card()
        {
            this.name = "søren hansen";
            this.number = CreateNumber();
        }

        protected virtual string CreateNumber()//doesnt do anything here
        {
            return string.Empty;
        }

    }
}
