using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BouncingBalls
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Game GameEngine;
        bool[] heldKeys = new bool[256];

        public GameWindow()
        {
            InitializeComponent();
        }

        private void GameWin_Loaded(object sender, RoutedEventArgs e)
        {
            int width = (int)MainGrid.ActualWidth;
            int height = (int)MainGrid.ActualHeight;

            Player player = new Player(width / 2 - 16, 0, width);

            List<Ball> startBalls = new List<Ball>();

            startBalls.Add(new Ball(50, new Point(width / 2, height / 3), new Point(1, -0.65), Colors.Aqua, 3));

            GameEngine = new Game(MainGrid, player, startBalls);

            GameLoop();
        }

        private async void GameLoop()
        {
            bool isWin = false;
            bool endGame = false;
            while(!endGame)
            {
                if (GameEngine.MoveAllBalls()) endGame = true;
                else
                {
                    if (heldKeys[(int)Key.A]) GameEngine.Player.MoveLeft();
                    if (heldKeys[(int)Key.D]) GameEngine.Player.MoveRight();
                    if (!(heldKeys[(int)Key.A] ^ heldKeys[(int)Key.D])) GameEngine.Player.MakeStatic();
                    if (heldKeys[(int)Key.W])
                    {
                        GameEngine.Shoot();
                        heldKeys[(int)Key.W] = false;
                    }

                    if (GameEngine.Balls.Count == 0)
                    {
                        isWin = true;
                        endGame = true;
                    }
                }

                await Task.Delay(25);
            }

            if (isWin) MessageBox.Show("Congratulations you win!", "Win!", MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Sorry you lose!", "Loss!", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }

        private void GameWin_KeyDown(object sender, KeyEventArgs e)
        {
            heldKeys[(int)e.Key] = true;
        }

        private void GameWin_KeyUp(object sender, KeyEventArgs e)
        {
            heldKeys[(int)e.Key] = false;
        }
    }
}
