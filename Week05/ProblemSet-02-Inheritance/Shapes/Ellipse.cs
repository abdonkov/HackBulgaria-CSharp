using System;

namespace Shapes
{
    public class Ellipse : Shape
    {
        public double RadiusX { get; protected set; }
        public double RadiusY { get; protected set; }
        public Point Center { get; protected set; }

        public override System.Drawing.Color Color { get; set; }

        public Ellipse(double radiusX, double radiusY, Point center) 
            : this(radiusX,radiusY,center,System.Drawing.Color.White) { }

        public Ellipse(double radiusX, double radiusY, Point center, System.Drawing.Color color)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
            Center = new Point(center);
            Color = color;
        }

        public override double GetArea()
        {
            return Math.PI * RadiusX * RadiusY;
        }

        public override double GetPerimeter()
        {
            //Calculated using Ramanujan second aproximation
            double h = Math.Pow(RadiusX - RadiusY, 2) / Math.Pow(RadiusX + RadiusY, 2);
            double p = Math.PI * (RadiusX + RadiusY) * (1 + (3 * h) / (10 + Math.Sqrt(4 - (3 * h))));
            return p;
        }

        public override string ToString()
        {
            return string.Format("Ellipse[RadiusX: {0}, RadiusY: {1}, Center: {2}]", RadiusX, RadiusY, Center);
        }

        public override void Move(double x, double y)
        {
            Center.Move(x, y);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(Color);
            System.Drawing.PointF upperLeftPoint = new System.Drawing.PointF((float)(Center.X - RadiusX), (float)(Center.Y - RadiusY));
            System.Drawing.SizeF rectSize = new System.Drawing.SizeF((float)(2 * RadiusX), (float)(2 * RadiusY));
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(upperLeftPoint, rectSize);
            g.FillEllipse(solidBrush, rect);
            solidBrush.Dispose();
        }
    }
}
