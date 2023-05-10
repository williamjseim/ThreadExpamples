using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Square
    {
        protected double a = 0;

        public double Area { get { return CalculateArea(); } }
        public double Size { get { return CalculateSize(); } }

        public Square(double a)
        {
            this.a = a;
        }

        protected virtual double CalculateArea()
        {
            return a * a;
        }

        protected virtual double CalculateSize()
        {
            return (4 * a);
        }
    }
}
