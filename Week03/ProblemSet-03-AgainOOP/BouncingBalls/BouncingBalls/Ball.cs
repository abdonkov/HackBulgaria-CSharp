using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BouncingBalls
{
    class Ball : Canvas
    {
        private readonly double radius;
        private Point center;
        private Point direction;
        private Color color;
        private double speed;
        public double Radius { get { return radius; } }
        public Point Center { get { return center; } }
        public Point Direction 
        {
            get{ return direction; }
            set { direction = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public double Speed 
        { 
            get { return speed; }
            set { speed = value; }
        }

        public Ball(double radius, Point center, Point direction, Color color, double speed)
        {
            this.radius = radius;
            this.center = center;
            this.direction = direction;
            this.color = color;            
            this.speed = speed;

            Height = radius * 2;
            Width = radius * 2;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            VerticalAlignment = System.Windows.VerticalAlignment.Top;
            Margin = new Thickness(center.X - radius, center.Y - radius, 0, 0);

            PaintBall();
        }

        private void PaintBall()
        {
            Ellipse elipse = new Ellipse();
            elipse.Stroke = System.Windows.Media.Brushes.Black;
            elipse.StrokeThickness = 1;
            elipse.Fill = new SolidColorBrush(color);
            elipse.HorizontalAlignment = HorizontalAlignment.Left;
            elipse.VerticalAlignment = VerticalAlignment.Center;
            elipse.Width = radius * 2;
            elipse.Height = radius * 2;
            this.Children.Add(elipse);
        }

        public void Move(double leftEnd, double topEnd, double rightEnd, double downEnd)
        {
            double oldLeftMargin = Margin.Left;
            double oldTopMargin = Margin.Top;
            double leftMargin = Margin.Left + direction.X * speed;
            double topMargin = Margin.Top + direction.Y * speed;

            if (leftMargin < leftEnd)
            {
                leftMargin = leftEnd;
                direction.X *= -1;
            }
            else if (leftMargin > rightEnd - (radius * 2))
            {
                leftMargin = rightEnd - (radius * 2);
                direction.X *= -1;
            }

            if (topMargin < topEnd)
            {
                topMargin = topEnd;
                direction.Y *= -1;
            }
            else if (topMargin > downEnd - (radius * 2))
            {
                topMargin = downEnd - (radius * 2);
                direction.Y *= -1;
            }

            Margin = new Thickness(leftMargin, topMargin, 0, 0);

            center.Offset(leftMargin - oldLeftMargin, topMargin - oldTopMargin);
        }
    }
}
