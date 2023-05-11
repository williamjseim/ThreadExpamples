using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeMaskine
{
    internal class Applience
    {
        protected bool hasElectricity = false;
        bool on;
        protected bool On { get { return on; } set { on = TurnOn(value); } }

        private bool TurnOn(bool on)
        {
            if (on)
            if(hasElectricity)
            {
                return true;
            }
            return false;
        }

        public void ConnectElectricity(bool electricity)
        {
            this.hasElectricity = electricity;
        }
    }
}
