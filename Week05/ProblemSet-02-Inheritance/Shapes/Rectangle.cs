using System;

namespace Shapes
{
    public class Rectangle : Shape
    {
        public double Width { get; protected set; }
        public double Height { get; protected set; }
        public Point Center { get; protected set; }

        public override System.Drawing.Color Color { get; set; }

        public Rectangle(double width, double height, Point center) 
            : this(width, height, center, System.Drawing.Color.White) { }

        public Rectangle(double width, double height, Point center, System.Drawing.Color color)
        {
            Width = width;
            Height = height;
            Center = new Point(center);
            Color = color;
        }

        public override double GetArea()
        {
            return Width * Height;
        }

        public override double GetPerimeter()
        {
            return 2 * (Width + Height);
        }

        public override string ToString()
        {
            return string.Format("Rectangle[Width: {0}, Height: {1}, Center: {2}]", Width, Height, Center);
        }

        public override void Move(double x, double y)
        {
            Center.Move(x, y);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(Color);
            System.Drawing.PointF upperLeftPoint = new System.Drawing.PointF((float)(Center.X - Width / 2), (float)(Center.Y - Height / 2));
            System.Drawing.SizeF rectSize = new System.Drawing.SizeF((float)Width, (float)Height);
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(upperLeftPoint, rectSize);
            g.FillRectangle(solidBrush, rect);
            solidBrush.Dispose();
        }
    }
}
