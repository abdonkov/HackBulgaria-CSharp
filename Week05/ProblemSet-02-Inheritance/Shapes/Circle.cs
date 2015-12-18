namespace Shapes
{
    public class Circle : Ellipse
    {
        private double radius;
        public double Radius
        {
            get { return radius; }
            protected set
            {
                RadiusX = value;
                RadiusY = value;
                radius = value;
            }
        }

        public Circle(double radius, Point center) 
            : this(radius, center, System.Drawing.Color.White) { }

        public Circle(double radius, Point center, System.Drawing.Color color) : base(radius, radius, center, color)
        {
            Radius = radius;
        }

        public override string ToString()
        {
            return string.Format("Circle[Radius: {0}, Center: {1}]", Radius, Center);
        }

        public override void Move(double x, double y)
        {
            Center.Move(x, y);
        }
    }
}
