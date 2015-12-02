using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFigures
{
    class LineSegment
    {
        private readonly Point firstPoint;
        private readonly Point secondPoint;
        public Point FirstPoint { get { return firstPoint; } }
        public Point SecondPoint { get { return secondPoint; } }

        public LineSegment(Point p1, Point p2)
        {
            if (p1 == p2) throw new ArgumentException("Cannot create a line segment with zero length");
            firstPoint = p1;
            secondPoint = p2;
        }

        public LineSegment(LineSegment ls)
        {
            firstPoint = ls.firstPoint;
            secondPoint = ls.secondPoint;
        }

        public double GetLength()
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
        }

        public override string ToString()
        {
            return string.Format("Line[({0}, {1}), ({2}, {3})]", firstPoint.X, firstPoint.Y, secondPoint.X, secondPoint.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is LineSegment)
            {
                LineSegment ls = obj as LineSegment;
                if (!firstPoint.Equals(ls.firstPoint)) return false;
                if (!secondPoint.Equals(ls.secondPoint)) return false;

                return true;
            }
            else return false;
        }

        public static bool operator ==(LineSegment ls1, LineSegment ls2)
        {
            return ls1.Equals(ls2);
        }

        public static bool operator !=(LineSegment ls1, LineSegment ls2)
        {
            return !ls1.Equals(ls2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + firstPoint.GetHashCode();
                hash = hash * 23 + secondPoint.GetHashCode();
                return hash;
            }
        }

        public static bool operator <(LineSegment ls1, LineSegment ls2)
        {
            return ls1.GetLength() < ls2.GetLength();
        }

        public static bool operator >(LineSegment ls1, LineSegment ls2)
        {
            return ls1.GetLength() > ls2.GetLength();
        }

        public static bool operator <=(LineSegment ls1, LineSegment ls2)
        {
            return ls1.GetLength() <= ls2.GetLength();
        }

        public static bool operator >=(LineSegment ls1, LineSegment ls2)
        {
            return ls1.GetLength() >= ls2.GetLength();
        }

        public static bool operator <(LineSegment ls, double length)
        {
            return ls.GetLength() < length;
        }

        public static bool operator <(double length, LineSegment ls)
        {
            return length < ls.GetLength();
        }

        public static bool operator >(LineSegment ls, double length)
        {
            return ls.GetLength() > length;
        }

        public static bool operator >(double length, LineSegment ls)
        {
            return length > ls.GetLength();
        }

        public static bool operator <=(LineSegment ls, double length)
        {
            return ls.GetLength() <= length;
        }

        public static bool operator <=(double length, LineSegment ls)
        {
            return length <= ls.GetLength();
        }

        public static bool operator >=(LineSegment ls, double length)
        {
            return ls.GetLength() >= length;
        }

        public static bool operator >=(double length, LineSegment ls)
        {
            return length >= ls.GetLength();
        }
    }
}
