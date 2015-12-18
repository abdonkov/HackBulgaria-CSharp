using System.Drawing;

namespace Shapes
{
    public abstract class Shape : IMovable, IDisplayable
    {
        public abstract Color Color { get; set; }

        public abstract void Draw(Graphics g);
        public abstract double GetArea();
        public abstract double GetPerimeter();
        public abstract void Move(double x, double y);
    }
}
