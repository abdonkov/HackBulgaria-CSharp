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

namespace APMCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        double apm;
        int numberOfClicks;
        Stopwatch sw;
        Thread apmCounter;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            apm = 0;
            numberOfClicks = 0;
            sw = Stopwatch.StartNew();
            apmCounter = new Thread
                (() =>
                    {
                        while (true)
                        {
                            apm = numberOfClicks / sw.Elapsed.TotalMinutes;
                            this.Dispatcher.Invoke(() =>
                                {
                                    UpdateLabelContent();
                                });
                            Thread.Sleep(1000);
                        }
                    }
                );
            apmCounter.Start();
        }

        private void UpdateLabelContent()
        {
            apmLabel.Content = $"Actions per minute: {apm:N4}";
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            numberOfClicks++;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            apmCounter.Abort();
        }
    }
}
