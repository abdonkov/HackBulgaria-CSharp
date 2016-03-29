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
    class Game
    {
        private readonly double width;
        private readonly double height;
        private readonly Grid mainGrid;
        private readonly Player player;
        private List<Ball> balls;
        private bool canShoot;
        public double Width { get { return width; } }
        public double Height { get { return height; } }
        public Grid MainGrid { get { return mainGrid; } }
        public Player Player { get { return player; } }
        public List<Ball> Balls
        {
            get { return balls; }
            set { balls = value; }
        }

        public Game(Grid mainGrid, Player player, List<Ball> balls)
        {
            this.width = mainGrid.ActualWidth;
            this.height = mainGrid.ActualHeight;

            this.mainGrid = mainGrid;

            this.player = player;
            this.mainGrid.Children.Add(player);

            this.balls = new List<Ball>();
            foreach (Ball ball in balls)
            {
                this.balls.Add(ball);
                this.mainGrid.Children.Add(ball);
            }

            canShoot = true;
        }

        public bool MoveAllBalls()
        {
            bool IsPlayerHit = false;
            foreach (var ball in balls)
            {
                ball.Move(0, 0, width, height);

                if (!IsPlayerHit)
                {
                    if (CheckForPlayerHit(ball, player))
                    {
                        IsPlayerHit = true;
                    }
                }
            }
            return IsPlayerHit;
        }

        public bool CheckForPlayerHit(Ball ball, Player player)
        {
            double playerTop = height - player.Height + 1;
            if (ball.Center.Y + ball.Radius >= playerTop)
            {
                double leftMost = player.PosX - player.SpriteWidth / 2;
                double rightMost = player.PosX + player.SpriteWidth / 2;

                if (ball.Center.X >= leftMost && ball.Center.X <= rightMost) return true;

                if (ball.Center.Y < playerTop)
                {
                    double distance = 0;
                    if (ball.Center.X < leftMost) distance = Math.Sqrt(Math.Pow(ball.Center.X - leftMost, 2) + Math.Pow(ball.Center.Y - playerTop, 2));
                    else distance = Math.Sqrt(Math.Pow(ball.Center.X - rightMost, 2) + Math.Pow(ball.Center.Y - playerTop, 2));

                    if (distance <= ball.Radius) return true;

                    else return false;
                }
                else
                {
                    if (ball.Center.X < leftMost) return ball.Center.X + ball.Radius >= leftMost;
                    else return ball.Center.X - ball.Radius <= rightMost;
                }
            }
            else return false;
        }

        public void BallDivide(Ball ballToDevide)
        {
            if (ballToDevide.Radius >= 10)
            {
                double oldRadius = ballToDevide.Radius;
                Point oldCenter = ballToDevide.Center;
                Point oldDirection = ballToDevide.Direction;
                Color oldColor = ballToDevide.Color;
                double oldSpeed = ballToDevide.Speed;

                double leftBallRadius = oldRadius / 2;
                Point leftBallCenter = new Point(oldCenter.X - oldRadius / 2, oldCenter.Y);
                Point leftBallDirection = new Point(-Math.Abs(oldDirection.X), oldDirection.Y);
                Color leftBallColor = oldColor;
                double leftBallSpeed = oldSpeed;

                Ball leftBall = new Ball(leftBallRadius, leftBallCenter, leftBallDirection, leftBallColor, leftBallSpeed);

                double rightBallRadius = oldRadius / 2;
                Point rightBallCenter = new Point(oldCenter.X + oldRadius / 2, oldCenter.Y);
                Point rightBallDirection = new Point(Math.Abs(oldDirection.X), oldDirection.Y);
                Color rightBallColor = oldColor;
                double rightBallSpeed = oldSpeed;

                Ball rightBall = new Ball(rightBallRadius, rightBallCenter, rightBallDirection, rightBallColor, rightBallSpeed);

                balls.Remove(ballToDevide);
                mainGrid.Children.Remove(ballToDevide);
                balls.Add(leftBall);
                mainGrid.Children.Add(leftBall);
                balls.Add(rightBall);
                mainGrid.Children.Add(rightBall);
            }
            else
            {
                mainGrid.Children.Remove(ballToDevide);
                balls.Remove(ballToDevide);
            }
        }

        public async void Shoot()
        {
            if (canShoot)
            {
                canShoot = false;

                Harpoon harpoon = new Harpoon(player.PosX, height - 32, height, 5, 5);
                mainGrid.Children.Insert(0, harpoon);

                while (harpoon.Move())
                {
                    if (CheckForBallHit(harpoon.PosX, harpoon.PosY, harpoon.Width / 2)) break;
                    await Task.Delay(25);
                }

                mainGrid.Children.Remove(harpoon);

                canShoot = true;
            }
        }

        public bool CheckForBallHit(double posX, double posY, double radiusOfEffect)
        {
            foreach (var ball in balls)
            {
                if (ball.Center.X - ball.Radius <= posX + radiusOfEffect && ball.Center.X + ball.Radius >= posX - radiusOfEffect)
                {
                    if (ball.Center.Y >= posY)
                    {
                        BallDivide(ball);
                        return true;
                    }
                    else if (ball.Center.Y + ball.Radius >= posY)
                    {
                        double distance = Math.Sqrt(Math.Pow(ball.Center.X - posX, 2) + Math.Pow(ball.Center.Y - posY, 2));
                        if (distance <= ball.Radius)
                        {
                            BallDivide(ball);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
