namespace Shapes
{
    public class Square : Rectangle
    {
        private double side;
        public double Side
        {
            get { return side; }
            protected set
            {
                Width = value;
                Height = value;
                side = value;
            }
        }

        public Square(double side, Point center) 
            : this(side, center, System.Drawing.Color.White) { }

        public Square(double side, Point center, System.Drawing.Color color) : base(side, side, center, color)
        {
            Side = side;
        }


        public override string ToString()
        {
            return string.Format("Square[Side: {0}, Center: {1}]", Side, Center);
        }

        public override void Move(double x, double y)
        {
            Center.Move(x, y);
        }
    }
}
