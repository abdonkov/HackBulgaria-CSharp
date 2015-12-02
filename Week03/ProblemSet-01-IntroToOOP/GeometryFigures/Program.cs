using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFigures
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p1 = new Point(1.2, 2.1);
            Point p2 = new Point(5.4, 7.2);

            Console.WriteLine("{0} + {1} = {2}", p1, p2, p1 + p2);

            Console.WriteLine();

            LineSegment ls1 = new LineSegment(new Point(1, 1), new Point(1, 5));
            LineSegment ls2 = new LineSegment(new Point(2, 3), new Point(4, 5));

            Console.WriteLine("{0} -> Length: {1}", ls1, ls1.GetLength());
            Console.WriteLine("{0} -> Length: {1}", ls2, ls2.GetLength());

            Console.WriteLine();

            Rectangle rect = new Rectangle(p1, p2);
            Rectangle rect2 = new Rectangle(p2, p1);

            Console.WriteLine("Is {0} equal to {1} -> {2}", rect, rect2, rect.Equals(rect2));
            Console.WriteLine();
            Console.WriteLine("Rectangle from {0} and {1} has these properties:", p1, p2);
            Console.WriteLine("Width: {0}", rect.Width);
            Console.WriteLine("Height: {0}", rect.Height);
            Console.WriteLine("Lower Left: {0}", rect.LowerLeftPoint);
            Console.WriteLine("Lower Rigth: {0}", rect.LowerRightPoint);
            Console.WriteLine("Upper Left: {0}", rect.UpperLeftPoint);
            Console.WriteLine("Upper Rigth: {0}", rect.UpperRightPoint);
            Console.WriteLine("Perimeter: {0}", rect.GetPerimeter());
            Console.WriteLine("Area: {0}", rect.GetArea());
            Console.WriteLine("Center: {0}", rect.Center);

            Console.WriteLine();

            Vector v1 = new Vector(1, 2, 3, 4);
            Vector v2 = new Vector(7, 3, 8, 1);

            Console.WriteLine("{0} + {1} = {2}", v1, v2, v1 + v2);
            Console.WriteLine("{0} - {1} = {2}", v1, v2, v1 - v2);
            Console.WriteLine("{0} * {1} = {2}", v1, v2, v1 * v2);
            Console.WriteLine("{0} - {1} = {2}", v1, 0.2, v1 - 0.2);
            Console.WriteLine("{0} * {1} = {2}", v1, 0.2, v1 * 0.2);
            
            Console.ReadKey();
        }
    }
}
