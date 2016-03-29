using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BouncingBalls
{
    class Harpoon : Image
    {
        private readonly double posX;
        private double posY;
        private readonly double height;
        private readonly double width;
        private readonly double change;
        public double PosX { get { return posX; } }
        public double PosY { get { return posY; } }

        public Harpoon(double posX, double posY, double height, double width, double change)
        {
            this.posX = posX;
            this.posY = posY;
            this.height = height;
            this.width = width;
            this.change = change;

            Height = height;
            Width = width;
            VerticalAlignment = System.Windows.VerticalAlignment.Top;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            Margin = new Thickness(posX - width / 2, height - posY, 0, 0);
            
            Stretch = System.Windows.Media.Stretch.Fill;

            Source = new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Harpoon.png"));
        }

        public bool Move()
        {
            posY -= change;
            if (posY < 0)
            {
                posY = 0;
                Margin = new Thickness(Margin.Left, posY, 0, 0);
                return false;
            }

            Margin = new Thickness(Margin.Left, posY, 0, 0);
            return true;
        }
    }
}
