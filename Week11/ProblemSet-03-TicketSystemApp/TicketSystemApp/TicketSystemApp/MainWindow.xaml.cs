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
using TicketSystem;

namespace TicketSystemApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TicketDB dbContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Focus();
            dbContext = new TicketDB();

            //Auto user1 login
            //usernameTextBox.Text = "user1";
            //passwordBox.Password = "password";
            //loginButton_Click(sender, e);

            //Auto admin1 login
            usernameTextBox.Text = "admin1";
            passwordBox.Password = "password";
            loginButton_Click(sender, e);
        }

        private void textBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginButton_Click(sender, e);
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;
            User loginUser;
            bool userExists = false;
            try
            {
               userExists  = TicketSystemSecurity.DoesUserExist(dbContext, username, password, out loginUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to database! Exception message:\n{ex.Message}");
                return;
            }

            if (userExists)
            {
                if (loginUser.IsAdmin)
                {
                    var adminWindow = new AdminWindow(dbContext, loginUser);
                    this.Hide();
                    adminWindow.ShowDialog();
                    this.Close();
                }
                else
                {
                    var normalUserWindow = new NormalUserWindow(dbContext, loginUser);
                    this.Hide();
                    normalUserWindow.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Incorrect username or password!");
                usernameTextBox.Text = string.Empty;
                passwordBox.Password = string.Empty;
                usernameTextBox.Focus();
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
