using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Trapez : Square
    {
        protected double b;
        private double c;
        private double d;
        public Trapez(double a, double b, double c, double d ) : base( a )
        {
            this.b = b;
            this.c = c;
            this.d = d;
        }

        double CalculateS()
        {
            return (a + b - c + d) / 2;
        }

        double CalculateH()
        {
            double s = CalculateS();
            double square = s * (s - a + c) * (s - b) * (s - d);
            return 2 / a - c * Math.Sqrt(square);
        }

        protected override double CalculateArea()
        {
            return CalculateH();
        }
    }
}
