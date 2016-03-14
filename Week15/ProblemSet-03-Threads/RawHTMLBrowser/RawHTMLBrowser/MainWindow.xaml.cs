using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace RawHTMLBrowser
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

        bool fetchingInProgress;
        Thread fetchThread;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fetchingInProgress = false;
            fetchThread = new Thread(() => { });
        }

        private void FetchSite(string url)
        {
            WebClient wc = new WebClient();
            string pageHTML = string.Empty;
            try
            {
                pageHTML = wc.DownloadString(url);
            }
            catch (WebException ex)
            {
                MessageBox.Show($"Connection is not possible! Please check your internet connection and site url!\n Error message: {ex.Message}");
                return;
            }

            this.Dispatcher.Invoke(() =>
            {
                htmlTextBlock.Text = pageHTML;
                fetchButton.Content = "Fetch site";
            });

            fetchingInProgress = false;
        }

        private void fetchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!fetchingInProgress)
            {
                Uri uriResult;
                bool validURL = Uri.TryCreate(urlTextBox.Text.Trim(), UriKind.Absolute, out uriResult)
                                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (validURL)
                {
                    fetchThread = new Thread
                        (() =>
                        {
                            string url = this.Dispatcher.Invoke(() => urlTextBox.Text);
                            FetchSite(url);
                        });
                    fetchingInProgress = true;
                    fetchThread.Start();
                    fetchButton.Content = "Cancel";
                }
                else MessageBox.Show("Url is invalid!");
            }
            else
            {
                fetchThread.Abort();
                fetchingInProgress = false;
                fetchButton.Content = "Fetch site";
            }
        }
    }
}
