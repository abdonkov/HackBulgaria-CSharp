using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFigures
{
    class Point
    {
        private readonly double x;
        private readonly double y;
        public static readonly Point Origin = new Point(0, 0);
        public double X { get { return x; } }
        public double Y { get { return y; } }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point()
        {
            x = origin.x;
            y = origin.y;
        }

        public Point(Point p)
        {
            x = p.x;
            y = p.y;
        }

        public override string ToString()
        {
            return string.Format("Point({0}, {1})", x, y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                Point p = obj as Point;
                if (!x.Equals(p.x)) return false;
                if (!y.Equals(p.y)) return false;

                return true;
            }
            else return false;
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !p1.Equals(p2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }

        public static LineSegment operator +(Point p1, Point p2)
        {
            return new LineSegment(p1, p2);
        }
    }
}
