using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Triangle : Square
    {
        public double b = 0;
        public double c = 0;
        public Triangle(double a, double b, double c) : base(a)
        {
            this.b = b;
            this.c = c;
        }

        protected override double CalculateArea()
        {
            return 1 / 2 * a * b;
        }
    }
}
