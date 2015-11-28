using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonCircumference
{
    class Program
    {
        static float CalcCircumference(PointF[] points)
        {
            float perimeter = 0;
            if (points.Length < 3) throw new ArgumentException();
            PointF firstPoint = points[0];
            PointF lastPoint = points[0];

            for (int i = 1; i < points.Length; i++)
            {
                perimeter += (float)Math.Sqrt(Math.Pow(points[i].X - lastPoint.X, 2) + Math.Pow(points[i].Y - lastPoint.Y, 2));
                lastPoint = points[i];
            }

            perimeter += (float)Math.Sqrt(Math.Pow(firstPoint.X - lastPoint.X, 2) + Math.Pow(firstPoint.Y - lastPoint.Y, 2));


            return perimeter;
        }

        static void Main(string[] args)
        {
            PointF[] polygonPoints = new PointF[]
            {
                new PointF((float)0.2, (float)2.5),
                new PointF((float)1.2, (float)2.5),
                new PointF((float)2.2, (float)2.5),
                new PointF((float)3.2, (float)2.5),
                new PointF((float)4.2, (float)2.5),
                new PointF((float)5.2, (float)2.5),
                new PointF((float)6.5, (float)2.5)
            };

            Console.WriteLine(CalcCircumference(polygonPoints));
            Console.ReadKey();
        }
    }
}
