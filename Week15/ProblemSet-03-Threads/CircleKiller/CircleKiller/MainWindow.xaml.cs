using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CircleKiller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int killedCircles;
        private double apm;
        Stopwatch sw;
        Thread circleSpawner;
        Random rand;
        ManualResetEvent circleKilled;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            killedCircles = 0;
            apm = 0;
            sw = new Stopwatch();
            sw.Start();
            rand = new Random();
            circleKilled = new ManualResetEvent(false);
            GameLoop();
        }

        private void GameLoop()
        {
            circleSpawner = new Thread
                (() =>
                {
                    while (true)
                    {
                        Canvas curCircle = null;
                        this.Dispatcher.Invoke(() => curCircle = CircleDrawer());
                        circleKilled.Reset();
                        circleKilled.WaitOne(1000);
                        this.Dispatcher.Invoke(() =>
                        {
                            mainGrid.Children.Remove(curCircle);
                            UpdateStatistics();
                        });
                    }
                });
            circleSpawner.Start();
        }

        private Canvas CircleDrawer()
        {
            Canvas circlePlaceholder = new Canvas();
            var circle = new Ellipse()
            {
                Margin = new Thickness(rand.NextDouble() * (this.ActualWidth - 50),
                                       50 + rand.NextDouble() * (this.ActualHeight - 120),
                                       0,
                                       0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 30,
                Width = 30,
                Fill = Brushes.Blue
            };

            circlePlaceholder.Children.Add(circle);
            circlePlaceholder.MouseUp += circlePlaceholder_MouseUp;
            mainGrid.Children.Add(circlePlaceholder);

            return circlePlaceholder;
        }

        private void UpdateStatistics()
        {
            apm = killedCircles / sw.Elapsed.TotalMinutes;
            statisticsLabel.Content = $"Killed Circles: {killedCircles} | Actions per minute {apm:N4}";
        }

        private void circlePlaceholder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            killedCircles++;
            var circlePlaceholder = sender as Canvas;

            circlePlaceholder.MouseUp -= circlePlaceholder_MouseUp;

            circleKilled.Set();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            circleSpawner.Abort();
        }
    }
}
