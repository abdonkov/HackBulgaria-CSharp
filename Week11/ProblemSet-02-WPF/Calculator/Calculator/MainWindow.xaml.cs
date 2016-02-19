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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool hasToRemoveZero = true;
        bool dotInserted = false;
        double firstOperand = 0;
        string function = string.Empty;

        public MainWindow()
        {
            Application.Current.Resources.Source = new Uri("/GrayTheme.xaml", UriKind.RelativeOrAbsolute);
            InitializeComponent();
        }

        private void UpdateCalcBackupTextBlock()
        {
            if (function != string.Empty)
            {
                backupTextBlock.Text = firstOperand.ToString() + " " + function;
            }
            else
            {
                backupTextBlock.Text = string.Empty;
            }
        }

        private void num0Button_Click(object sender, RoutedEventArgs e)
        {
            if (!hasToRemoveZero) calcTextBlock.Text += "0";
        }

        private void num1Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "1";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "1";
            }
        }

        private void num2Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "2";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "2";
            }
        }

        private void num3Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "3";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "3";
            }
        }

        private void num4Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "4";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "4";
            }
        }

        private void num5Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "5";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "5";
            }
        }

        private void num6Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "6";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "6";
            }
        }

        private void num7Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "7";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "7";
            }
        }

        private void num8Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "8";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "8";
            }
        }

        private void num9Button_Click(object sender, RoutedEventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBlock.Text = "9";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBlock.Text += "9";
            }
        }

        private void dotButton_Click(object sender, RoutedEventArgs e)
        {
            if (!dotInserted)
            {
                calcTextBlock.Text += ".";
                dotInserted = true;
                hasToRemoveZero = false;
            }
        }

        private void signChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (calcTextBlock.Text.First() == '-') calcTextBlock.Text = calcTextBlock.Text.Substring(1);
            else calcTextBlock.Text = "-" + calcTextBlock.Text;
        }

        private void ceButton_Click(object sender, RoutedEventArgs e)
        {
            calcTextBlock.Text = "0";
            hasToRemoveZero = true;
            dotInserted = false;
            firstOperand = 0;
            function = string.Empty;
            UpdateCalcBackupTextBlock();
        }

        private void cButton_Click(object sender, RoutedEventArgs e)
        {
            calcTextBlock.Text = "0";
            hasToRemoveZero = true;
            dotInserted = false;
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            char deletedChar = calcTextBlock.Text.Last();
            if (deletedChar == '.') dotInserted = false;

            calcTextBlock.Text = calcTextBlock.Text.Substring(0, calcTextBlock.Text.Length - 1);

            if (calcTextBlock.Text.Length == 0) calcTextBlock.Text = "0";
            else if (calcTextBlock.Text.Length == 1 && calcTextBlock.Text == "-") calcTextBlock.Text = "0";

            if (calcTextBlock.Text == "0") hasToRemoveZero = true;
        }

        private void divideButton_Click(object sender, RoutedEventArgs e)
        {
            if (function == string.Empty)
            {
                firstOperand = double.Parse(calcTextBlock.Text);
                cButton_Click(sender, e);
                function = "/";
            }
            else
            {
                equalButton_Click(sender, e);
                firstOperand = double.Parse(calcTextBlock.Text);
                cButton_Click(sender, e);
                function = "/";
            }
            UpdateCalcBackupTextBlock();
        }

        private void multiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (function == string.Empty)
            {
                firstOperand = double.Parse(calcTextBlock.Text);
                cButton_Click(sender, e);
                function = "*";
            }
            else
            {
                equalButton_Click(sender, e);
                firstOperand = double.Parse(calcTextBlock.Text);
                cButton_Click(sender, e);
                function = "*";
            }
            UpdateCalcBackupTextBlock();
        }

        private void subtractButton_Click(object sender, RoutedEventArgs e)
        {
            if (function == string.Empty)
            {
                firstOperand = double.Parse(calcTextBlock.Text);
                cButton_Click(sender, e);
                function = "-";
            }
            else
            {
                equalButton_Click(sender, e);
                firstOperand = double.Parse(calcTextBlock.Text);
                cButton_Click(sender, e);
                function = "-";
            }
            UpdateCalcBackupTextBlock();
        }

        private void sumButton_Click(object sender, RoutedEventArgs e)
        {
            if (function == string.Empty)
            {
                firstOperand = double.Parse(calcTextBlock.Text);
                cButton_Click(sender, e);
                function = "+";
            }
            else
            {
                equalButton_Click(sender, e);
                firstOperand = double.Parse(calcTextBlock.Text);
                cButton_Click(sender, e);
                function = "+";
            }
            UpdateCalcBackupTextBlock();
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            if (function != string.Empty)
            {
                double result = 0;
                try
                {
                    switch (function)
                    {
                        case "/":
                            {
                                result = firstOperand / double.Parse(calcTextBlock.Text);
                                break;
                            }
                        case "*":
                            {
                                result = firstOperand * double.Parse(calcTextBlock.Text);
                                break;
                            }
                        case "-":
                            {
                                result = firstOperand - double.Parse(calcTextBlock.Text);
                                break;
                            }
                        case "+":
                            {
                                result = firstOperand + double.Parse(calcTextBlock.Text);
                                break;
                            }
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    ceButton_Click(sender, e);
                    calcTextBlock.Text = result.ToString();
                    if (result != 0) hasToRemoveZero = false;
                    dotInserted = calcTextBlock.Text.Contains(".");
                    UpdateCalcBackupTextBlock();
                    if (double.IsInfinity(result) || double.IsNaN(result))
                    {
                        calcTextBlock.Text = "0";
                        backupTextBlock.Text = "Cannot divide by zero!";
                        hasToRemoveZero = true;
                    }
                }
            }
        }

        private void sqrtButton_Click(object sender, RoutedEventArgs e)
        {
            backupTextBlock.Text = "";
            if (function != string.Empty)
            {
                equalButton_Click(sender, e);
            }

            double result = Math.Sqrt(double.Parse(calcTextBlock.Text));
            calcTextBlock.Text = result.ToString();
            if (double.IsInfinity(result) || double.IsNaN(result))
            {
                calcTextBlock.Text = "0";
                backupTextBlock.Text = "Invalid operation!";
                hasToRemoveZero = true;
            }
            if (calcTextBlock.Text.Contains(".")) dotInserted = true;
            else dotInserted = false;
            if (calcTextBlock.Text == "0") hasToRemoveZero = true;
        }

        private void log2Button_Click(object sender, RoutedEventArgs e)
        {
            backupTextBlock.Text = "";
            if (function != string.Empty)
            {
                equalButton_Click(sender, e);
            }

            double result = Math.Log(double.Parse(calcTextBlock.Text), 2);
            calcTextBlock.Text = result.ToString();
            if (double.IsInfinity(result) || double.IsNaN(result))
            {
                calcTextBlock.Text = "0";
                backupTextBlock.Text = "Invalid operation!";
                hasToRemoveZero = true;
            }
            if (calcTextBlock.Text.Contains(".")) dotInserted = true;
            else dotInserted = false;
            if (calcTextBlock.Text == "0") hasToRemoveZero = true;
        }

        private void xPowerOf2Button_Click(object sender, RoutedEventArgs e)
        {
            backupTextBlock.Text = "";
            if (function != string.Empty)
            {
                equalButton_Click(sender, e);
            }

            calcTextBlock.Text = Math.Pow(double.Parse(calcTextBlock.Text), 2).ToString();
            if (calcTextBlock.Text.Contains(".")) dotInserted = true;
            else dotInserted = false;
            if (calcTextBlock.Text == "0") hasToRemoveZero = true;
        }

        private void xRecipricalButton_Click(object sender, RoutedEventArgs e)
        {
            backupTextBlock.Text = "";
            if (function != string.Empty)
            {
                equalButton_Click(sender, e);
            }

            double result = 1.0 / double.Parse(calcTextBlock.Text);
            calcTextBlock.Text = result.ToString();
            if (double.IsInfinity(result) || double.IsNaN(result))
            {
                calcTextBlock.Text = "0";
                backupTextBlock.Text = "Cannot divide by zero!";
                hasToRemoveZero = true;
            }
            if (calcTextBlock.Text.Contains(".")) dotInserted = true;
            else dotInserted = false;
            if (calcTextBlock.Text == "0") hasToRemoveZero = true;
        }

        private void themeChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var curSource = Application.Current.Resources.Source;
            switch(curSource.OriginalString)
            {
                case "/GrayTheme.xaml":
                    Application.Current.Resources.Source = new Uri("/BlueTheme.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "/BlueTheme.xaml":
                    Application.Current.Resources.Source = new Uri("/GreenTheme.xaml", UriKind.RelativeOrAbsolute);
                    break;
                case "/GreenTheme.xaml":
                    Application.Current.Resources.Source = new Uri("/GrayTheme.xaml", UriKind.RelativeOrAbsolute);
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    num0Button_Click(sender, e);
                    break;
                case Key.D1:
                case Key.NumPad1:
                    num1Button_Click(sender, e);
                    break;
                case Key.D2:
                case Key.NumPad2:
                    num2Button_Click(sender, e);
                    break;
                case Key.D3:
                case Key.NumPad3:
                    num3Button_Click(sender, e);
                    break;
                case Key.D4:
                case Key.NumPad4:
                    num4Button_Click(sender, e);
                    break;
                case Key.D5:
                case Key.NumPad5:
                    num5Button_Click(sender, e);
                    break;
                case Key.D6:
                case Key.NumPad6:
                    num6Button_Click(sender, e);
                    break;
                case Key.D7:
                case Key.NumPad7:
                    num7Button_Click(sender, e);
                    break;
                case Key.D8:
                case Key.NumPad8:
                    num8Button_Click(sender, e);
                    break;
                case Key.D9:
                case Key.NumPad9:
                    num9Button_Click(sender, e);
                    break;
                case Key.OemPeriod:
                case Key.OemComma:
                case Key.Decimal:
                    dotButton_Click(sender, e);
                    break;
                case Key.Divide:
                    divideButton_Click(sender, e);
                    break;
                case Key.Multiply:
                    multiplyButton_Click(sender, e);
                    break;
                case Key.Subtract:
                    subtractButton_Click(sender, e);
                    break;
                case Key.Add:
                    sumButton_Click(sender, e);
                    break;
                case Key.Enter:
                    equalButton_Click(sender, e);
                    break;
            }
        }
    }
}
