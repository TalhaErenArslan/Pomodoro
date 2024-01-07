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
using System.Windows.Threading;
using app;

namespace app
{
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Content = new LoginPage();
            this.Content = frame;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Content = new MainWindow();
            this.Content = frame;
        }
    }

}