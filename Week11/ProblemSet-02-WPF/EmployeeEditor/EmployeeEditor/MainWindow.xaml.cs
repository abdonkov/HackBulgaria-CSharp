using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace EmployeeEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HackCompanyEntities dbContext = new HackCompanyEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource employeeViewSource =
                (CollectionViewSource)(this.FindResource("employeeViewSource"));

            dbContext.Employees.ToList();

            employeeViewSource.Source = dbContext.Employees.Local;
        }

        private void employeeDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var employee = e.Row.Item as Employee;
            if (employee != null)
            {
                switch (e.Column.Header.ToString())
                {
                    case "First Name":
                        {
                            var textBox = e.EditingElement as TextBox;
                            employee.FirstName = textBox.Text;
                            break;
                        }
                    case "Last Name":
                        {
                            var textBox = e.EditingElement as TextBox;
                            employee.LastName = textBox.Text;
                            break;
                        }
                    case "Email":
                        {
                            var textBox = e.EditingElement as TextBox;
                            if (Regex.IsMatch(textBox.Text,
                                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                                    RegexOptions.IgnoreCase))
                            {
                                employee.Email = textBox.Text;
                            }
                            else
                            {
                                MessageBox.Show("Invalid email fromat!");
                                textBox.Text = employee.Email;
                            }
                            break;
                        }
                    case "Birth Date":
                        {
                            var datePicker = VisualTreeHelper.GetChild(e.EditingElement, 0) as DatePicker;
                            if (datePicker.SelectedDate < DateTime.Now
                                && datePicker.SelectedDate != null)
                            {
                                employee.BirthDate = (DateTime)datePicker.SelectedDate;
                            }
                            else
                            {
                                MessageBox.Show("Invalid date provided!");
                                datePicker.SelectedDate = employee.BirthDate;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dbContext.Dispose();
        }

        private void updateDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dbContext.SaveChanges();
                MessageBox.Show("Database updated successfully!");
            }
            catch (OptimisticConcurrencyException ex)
            {
                MessageBox.Show($"A problem occured trying to update database! {ex.Message}");
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
