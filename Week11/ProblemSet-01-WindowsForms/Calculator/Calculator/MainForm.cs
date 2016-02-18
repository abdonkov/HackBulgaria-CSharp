using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class MainForm : Form
    {
        bool hasToRemoveZero = true;
        bool dotInserted = false;
        double firstOperand = 0;
        string function = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        private void UpdateCalcBackupLabel()
        {
            if (function != string.Empty)
            {
                calcBackupLabel.Text = firstOperand.ToString() + " " + function;
            }
            else
            {
                calcBackupLabel.Text = string.Empty;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ActiveControl = calcTextBox;
            calcTextBox.Select(1, 0);
        }

        private void num0Button_Click(object sender, EventArgs e)
        {
            if (!hasToRemoveZero) calcTextBox.Text += "0";
        }

        private void num1Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "1";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "1";
            }
        }

        private void num2Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "2";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "2";
            }
        }

        private void num3Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "3";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "3";
            }
        }

        private void num4Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "4";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "4";
            }
        }

        private void num5Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "5";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "5";
            }
        }

        private void num6Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "6";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "6";
            }
        }

        private void num7Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "7";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "7";
            }
        }

        private void num8Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "8";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "8";
            }
        }

        private void num9Button_Click(object sender, EventArgs e)
        {
            if (hasToRemoveZero)
            {
                calcTextBox.Text = "9";
                hasToRemoveZero = false;
            }
            else
            {
                calcTextBox.Text += "9";
            }
        }

        private void dotButton_Click(object sender, EventArgs e)
        {
            if (!dotInserted)
            {
                calcTextBox.Text += ".";
                dotInserted = true;
                hasToRemoveZero = false;
            }
        }

        private void signChangeButton_Click(object sender, EventArgs e)
        {
            if (calcTextBox.Text.First() == '-') calcTextBox.Text = calcTextBox.Text.Substring(1);
            else calcTextBox.Text = "-" + calcTextBox.Text;
        }

        private void ceButton_Click(object sender, EventArgs e)
        {
            calcTextBox.Text = "0";
            hasToRemoveZero = true;
            dotInserted = false;
            firstOperand = 0;
            function = string.Empty;
            UpdateCalcBackupLabel();
        }

        private void cButton_Click(object sender, EventArgs e)
        {
            calcTextBox.Text = "0";
            hasToRemoveZero = true;
            dotInserted = false;
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            char deletedChar = calcTextBox.Text.Last();
            if (deletedChar == '.') dotInserted = false;

            calcTextBox.Text = calcTextBox.Text.Substring(0, calcTextBox.Text.Length - 1);

            if (calcTextBox.Text.Length == 0) calcTextBox.Text = "0";
            else if (calcTextBox.Text.Length == 1 && calcTextBox.Text == "-") calcTextBox.Text = "0";

            if (calcTextBox.Text == "0") hasToRemoveZero = true;
        }

        private void divideButton_Click(object sender, EventArgs e)
        {
            if (function != string.Empty)
            {
                firstOperand = double.Parse(calcTextBox.Text);
                cButton_Click(sender, e);
                function = "/";
            }
            else
            {
                equalButton_Click(sender, e);
                firstOperand = double.Parse(calcTextBox.Text);
                cButton_Click(sender, e);
                function = "/";
            }
            UpdateCalcBackupLabel();
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            if (function != string.Empty)
            {
                firstOperand = double.Parse(calcTextBox.Text);
                cButton_Click(sender, e);
                function = "*";
            }
            else
            {
                equalButton_Click(sender, e);
                firstOperand = double.Parse(calcTextBox.Text);
                cButton_Click(sender, e);
                function = "*";
            }
            UpdateCalcBackupLabel();
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            if (function != string.Empty)
            {
                firstOperand = double.Parse(calcTextBox.Text);
                cButton_Click(sender, e);
                function = "-";
            }
            else
            {
                equalButton_Click(sender, e);
                firstOperand = double.Parse(calcTextBox.Text);
                cButton_Click(sender, e);
                function = "-";
            }
            UpdateCalcBackupLabel();
        }

        private void sumButton_Click(object sender, EventArgs e)
        {
            if (function != string.Empty)
            {
                firstOperand = double.Parse(calcTextBox.Text);
                cButton_Click(sender, e);
                function = "+";
            }
            else
            {
                equalButton_Click(sender, e);
                firstOperand = double.Parse(calcTextBox.Text);
                cButton_Click(sender, e);
                function = "+";
            }
            UpdateCalcBackupLabel();
        }

        private void equalButton_Click(object sender, EventArgs e)
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
                                result = firstOperand / double.Parse(calcTextBox.Text);
                                break;
                            }
                        case "*":
                            {
                                result = firstOperand * double.Parse(calcTextBox.Text);
                                break;
                            }
                        case "-":
                            {
                                result = firstOperand - double.Parse(calcTextBox.Text);
                                break;
                            }
                        case "+":
                            {
                                result = firstOperand + double.Parse(calcTextBox.Text);
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
                    calcTextBox.Text = result.ToString();
                    if (result != 0) hasToRemoveZero = false;
                    dotInserted = calcTextBox.Text.Contains(".");
                    UpdateCalcBackupLabel();
                    if (double.IsInfinity(result) || double.IsNaN(result))
                    {
                        calcTextBox.Text = "0";
                        calcBackupLabel.Text = "Cannot divide by zero!";
                        hasToRemoveZero = true;
                    }
                }
            }
        }

        private void sqrtButton_Click(object sender, EventArgs e)
        {
            calcBackupLabel.Text = "";
            if (function != string.Empty)
            {
                equalButton_Click(sender, e);
            }

            double result = Math.Sqrt(double.Parse(calcTextBox.Text));
            calcTextBox.Text = result.ToString();
            if (double.IsInfinity(result) || double.IsNaN(result))
            {
                calcTextBox.Text = "0";
                calcBackupLabel.Text = "Invalid operation!";
                hasToRemoveZero = true;
            }
            if (calcTextBox.Text.Contains(".")) dotInserted = true;
            else dotInserted = false;
            if (calcTextBox.Text == "0") hasToRemoveZero = true;
        }

        private void log2Button_Click(object sender, EventArgs e)
        {
            calcBackupLabel.Text = "";
            if (function != string.Empty)
            {
                equalButton_Click(sender, e);
            }

            double result = Math.Log(double.Parse(calcTextBox.Text), 2);
            calcTextBox.Text = result.ToString();
            if (double.IsInfinity(result) || double.IsNaN(result))
            {
                calcTextBox.Text = "0";
                calcBackupLabel.Text = "Invalid operation!";
                hasToRemoveZero = true;
            }
            if (calcTextBox.Text.Contains(".")) dotInserted = true;
            else dotInserted = false;
            if (calcTextBox.Text == "0") hasToRemoveZero = true;
        }

        private void xPowerOf2Button_Click(object sender, EventArgs e)
        {
            calcBackupLabel.Text = "";
            if (function != string.Empty)
            {
                equalButton_Click(sender, e);
            }

            calcTextBox.Text = Math.Pow(double.Parse(calcTextBox.Text), 2).ToString();
            if (calcTextBox.Text.Contains(".")) dotInserted = true;
            else dotInserted = false;
            if (calcTextBox.Text == "0") hasToRemoveZero = true;
        }

        private void xRecipricalButton_Click(object sender, EventArgs e)
        {
            calcBackupLabel.Text = "";
            if (function != string.Empty)
            {
                equalButton_Click(sender, e);
            }

            double result = 1.0 / double.Parse(calcTextBox.Text);
            calcTextBox.Text = result.ToString();
            if (double.IsInfinity(result) || double.IsNaN(result))
            {
                calcTextBox.Text = "0";
                calcBackupLabel.Text = "Cannot divide by zero!";
                hasToRemoveZero = true;
            }
            if (calcTextBox.Text.Contains(".")) dotInserted = true;
            else dotInserted = false;
            if (calcTextBox.Text == "0") hasToRemoveZero = true;
        }
    }
}
