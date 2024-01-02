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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        private DispatcherTimer timer;
        private TimeSpan timeRemaining;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                DataContext = new RegisterViewModel();
                timeRemaining = new TimeSpan(0, 25, 0);

                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += Timer_Tick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            ellipsee.Stroke = Brushes.Green;
            // StartButton.Background = Brushes.Green;
            // StopButton.Background = Brushes.White;


        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            ellipsee.Stroke = Brushes.DarkRed;
            // StartButton.Background = Brushes.White;
            // StopButton.Background = Brushes.DarkRed;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            TimeLabel.Content = timeRemaining.ToString("mm\\:ss");

            timeRemaining = timeRemaining.Subtract(new TimeSpan(0, 0, 1));

            if (timeRemaining.TotalSeconds == 0)
            {
                timer.Stop();
                MessageBox.Show("Time's up!");
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Content = new LoginPage();
            this.Content = frame;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = TextInput.Text;

            if (!string.IsNullOrEmpty(inputText))
            {
                RadioListBox.Items.Add(inputText);
                TextInput.Clear();
            }
        }


    }
}
