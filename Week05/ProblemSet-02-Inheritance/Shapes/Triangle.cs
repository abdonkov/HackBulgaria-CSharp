using System;

namespace Shapes
{
    public class Triangle : Shape
    {
        public Point Vertex1 { get; protected set; }
        public Point Vertex2 { get; protected set; }
        public Point Vertex3 { get; protected set; }

        public override System.Drawing.Color Color { get; set; }

        public Triangle(Point vertex1, Point vertex2, Point vertex3) 
            : this(vertex1, vertex2, vertex3, System.Drawing.Color.White) { }

        public Triangle(Point vertex1, Point vertex2, Point vertex3, System.Drawing.Color color)
        {
            Vertex1 = new Point(vertex1);
            Vertex2 = new Point(vertex2);
            Vertex3 = new Point(vertex3);
            Color = color;
        }

        public override double GetArea()
        {
            return Math.Abs((Vertex1.X * (Vertex2.Y - Vertex3.Y) + Vertex2.X * (Vertex3.Y - Vertex1.Y) + Vertex3.X * (Vertex1.Y - Vertex2.Y)) / 2);
        }

        public override double GetPerimeter()
        {
            double a = Math.Sqrt(Math.Pow(Vertex1.X - Vertex2.X, 2) + Math.Pow(Vertex1.Y - Vertex2.Y, 2));
            double b = Math.Sqrt(Math.Pow(Vertex2.X - Vertex3.X, 2) + Math.Pow(Vertex2.Y - Vertex3.Y, 2));
            double c = Math.Sqrt(Math.Pow(Vertex3.X - Vertex1.X, 2) + Math.Pow(Vertex3.Y - Vertex1.Y, 2));
            return a + b + c;
        }

        public override string ToString()
        {
            return string.Format("Triangle[{0}, {1}, {2}]", Vertex1, Vertex2, Vertex3);
        }

        public override void Move(double x, double y)
        {
            Vertex1.Move(x, y);
            Vertex2.Move(x, y);
            Vertex3.Move(x, y);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(Color);
            System.Drawing.PointF[] points = new System.Drawing.PointF[3];
            points[0] = new System.Drawing.PointF((float)Vertex1.X, (float)Vertex1.Y);
            points[1] = new System.Drawing.PointF((float)Vertex2.X, (float)Vertex2.Y);
            points[2] = new System.Drawing.PointF((float)Vertex3.X, (float)Vertex3.Y);
            g.FillPolygon(solidBrush, points);
            solidBrush.Dispose();
        }
    }
}
