using System;

namespace Lesson11.fiveth
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Move(int dx, int dy)
        {
            this.X += dx;
            this.Y += dy;
        }

        public void Print()
        {
            Console.WriteLine($"Point({X}, {Y})");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Point(2, 3); 
            var p2 = p1; 
            p2.Move(5, 5); 
            p1.Print();
        }
    }
}
