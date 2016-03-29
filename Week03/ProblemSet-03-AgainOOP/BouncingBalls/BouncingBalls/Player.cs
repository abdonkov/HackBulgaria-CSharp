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
    class Player : Image
    {
        private double posX;
        private int minPos;
        private int maxPos;
        private readonly List<BitmapImage> leftMovePlayerImages;
        private readonly List<BitmapImage> rightMovePlayerImages;
        private int currentLeftMoveImage;
        private int currentRightMoveImage;
        private readonly BitmapImage backPlayerImage;
        private readonly double backSpriteWidth;
        private readonly double sideSpriteWidth;
        private double curSpriteWidth;
        public double PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        public double SpriteWidth { get { return curSpriteWidth; } }

        public Player(double posX, int minPos, int maxPos)
        {
            Height = 32;
            Width = 32;
            VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            this.posX = posX + 16;
            this.minPos = minPos + 16;
            this.maxPos = maxPos - 16;

            Margin = new Thickness(this.posX - 16, 0, 0, 0);

            backPlayerImage = new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Back.png"));

            leftMovePlayerImages = new List<BitmapImage>(4);
            leftMovePlayerImages.Add(new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Left-Move1.png")));
            leftMovePlayerImages.Add(new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Left-Steady.png")));
            leftMovePlayerImages.Add(new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Left-Move2.png")));
            leftMovePlayerImages.Add(new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Left-Steady.png")));

            rightMovePlayerImages = new List<BitmapImage>(4);
            rightMovePlayerImages.Add(new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Right-Move1.png")));
            rightMovePlayerImages.Add(new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Right-Steady.png")));
            rightMovePlayerImages.Add(new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Right-Move2.png")));
            rightMovePlayerImages.Add(new BitmapImage(new Uri("pack://application:,,,/BouncingBalls;component/Resources/Right-Steady.png")));

            currentLeftMoveImage = 0;
            currentRightMoveImage = 0;

            sideSpriteWidth = 16;
            backSpriteWidth = 20;

            Source = backPlayerImage;
            curSpriteWidth = backSpriteWidth;
        }

        public void MoveLeft()
        {
            posX -= 5;
            if (posX < minPos)
            {
                posX = minPos;
                MakeStatic();
                return;
            }
            Source = leftMovePlayerImages[currentLeftMoveImage];
            Margin = new Thickness(posX - 16, 0, 0, 0);
            if (++currentLeftMoveImage > 3) currentLeftMoveImage = 0;
            curSpriteWidth = sideSpriteWidth;
        }

        public void MoveRight()
        {
            posX += 5;
            if (posX > maxPos)
            {
                posX = maxPos;
                MakeStatic();
                return;
            }
            Source = rightMovePlayerImages[currentRightMoveImage];
            Margin = new Thickness(posX - 16, 0, 0, 0);
            if (++currentRightMoveImage > 3) currentRightMoveImage = 0;
            curSpriteWidth = sideSpriteWidth;

        }

        public void MakeStatic()
        {
            Source = backPlayerImage;
            curSpriteWidth = backSpriteWidth;
            currentLeftMoveImage = 0;
            currentRightMoveImage = 0;
        }
    }
}
