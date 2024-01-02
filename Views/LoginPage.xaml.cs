using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using System.Windows.Threading;

namespace app
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void SigninButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame frame = new Frame();
                frame.Content = new RegisterPage();
                this.Content = frame;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // mainWindow.Show();
            Frame frame = new Frame();
            frame.Content = new MainWindow();
            this.Content = frame;
            // LoginFrame.NavigationService.Navigate(new Uri("./Views/MainWindow.xaml", UriKind.Relative));
            //  this.Close();
        }
    }

    internal class UsernameTextBox
    {
        public static string Text { get; internal set; }
    }
}