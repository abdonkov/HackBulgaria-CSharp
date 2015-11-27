using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonArea
{
    class Program
    {
        static float CalcArea(PointF[] points)
        {
            float area = 0;
            if (points.Length < 3) throw new ArgumentException();
            PointF startPoint = points[0];
            PointF lastPoint = points[0];

            for (int i = 1; i < points.Length; i++)
            {
                area += lastPoint.X * points[i].Y - lastPoint.Y * points[i].X;
                lastPoint = points[i];
            }

            area += lastPoint.X * startPoint.Y - lastPoint.Y * startPoint.X;
            area /= 2;
            area = Math.Abs(area);

            return area;
        }

        static void Main(string[] args)
        {
            PointF[] polygonPoints = new PointF[]
            {
                new PointF((float)2, (float)2),
                new PointF((float)4, (float)10),
                new PointF((float)9, (float)7),
                new PointF((float)11, (float)2)
            };

            Console.WriteLine("{0:N4}", CalcArea(polygonPoints));
            Console.ReadKey();
        }
    }
}
