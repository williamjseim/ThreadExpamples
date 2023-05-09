using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MyBanker
{
    internal class ExpirableCard : Card
    {
        public DateOnly? expirationDate { get; set; }
        protected virtual DateOnly? CreateExpirationDate()
        {
            DateOnly expirationDate = new DateOnly(DateTime.Now.Year, 4, DateTime.Now.Day);
            expirationDate.AddYears(4);
            return expirationDate;
        }
        public ExpirableCard() : base()
        {
            expirationDate = CreateExpirationDate();
        }
        public override string ToString()
        {
            return $"{number} \n {name} \n {expirationDate}";
        }
    }
}
