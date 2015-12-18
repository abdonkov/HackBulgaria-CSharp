namespace Shapes
{
    public class Point : IMovable
    {
        public static readonly Point Origin = new Point(0, 0);
        public double X { get; protected set; }

        public double Y { get; protected set; }

        public Point() : this(0, 0) { }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public override string ToString()
        {
            return string.Format("Point[{0}, {1}]", X, Y);
        }

        public void Move(double x, double y)
        {
            X += x;
            Y += y;
        }
    }
}
