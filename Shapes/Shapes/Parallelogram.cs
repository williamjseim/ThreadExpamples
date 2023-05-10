using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    internal class Parallelogram : Square
    {
        private double b;
        private double angle;
        public Parallelogram(double a, double b, double angle) : base(a)
        {
            this.b = b;
            this.angle = angle;

        }

        protected override double CalculateArea()
        {
            return a * b * Math.Sin(CalculateRadians());
        }

        protected private double CalculateRadians()
        {
            double radians = angle * (Math.PI) / 180;
            return radians;
        }
    }
}
