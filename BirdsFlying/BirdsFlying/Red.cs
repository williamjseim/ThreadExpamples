using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdsFlying
{
    internal class Red : Bird, IFly
    {
        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write("#");
        }

        public void Fly()
        {
            Console.WriteLine("bird flys");
        }

        public override void SetAltitude(double altitude)
        {
            throw new NotImplementedException();
        }

        public override void SetLocation(double longitude, double latitude)
        {
            Position = new VectorI2D { x = (int)longitude, y = (int)latitude };

        }
    }
}
