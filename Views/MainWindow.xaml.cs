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
using app.Models;
using app.Models;

namespace app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        private DispatcherTimer timer;
        private TimeSpan timeRemaining;
        private bool isTimerRunning;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                // DataContext = new RegisterViewModel();
                DataContext = ((App)Application.Current).SharedViewModel;
                timeRemaining = new TimeSpan(0, 25, 0);
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Tick += Timer_Tick;
                UpdateUserTasksListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isTimerRunning)
            {
                timer.Start();
                ellipsee.Stroke = Brushes.Green;
                isTimerRunning = true;
            }
            // timer.Start();
            // StartButton.Background = Brushes.Green;
            // StopButton.Background = Brushes.White;


        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // timer.Stop();

            // StartButton.Background = Brushes.White;
            // StopButton.Background = Brushes.DarkRed;
            if (isTimerRunning)
            {
                SaveTaskToDatabase(); // Eğer durdurulduysa, görevi kaydet
                timer.Stop();
                isTimerRunning = false;
                ellipsee.Stroke = Brushes.DarkRed;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Content = timeRemaining.ToString("mm\\:ss");

            timeRemaining = timeRemaining.Subtract(new TimeSpan(0, 0, 1));

            if (timeRemaining.TotalSeconds == 0)
            {
                timer.Stop();
                isTimerRunning = false;

                // Süre bittiğinde görevi kaydet
                SaveTaskToDatabase();
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
                string username = ((App)Application.Current).SharedViewModel.LoggedInUsername;
                Todo newTask = new Todo
                {
                    username = username,
                    task = inputText,
                    time = TimeSpan.Zero
                };

                ((App)Application.Current).SharedViewModel.SaveTaskToDatabase(newTask);
                RadioListBox.Items.Add(inputText);
                TextInput.Clear();
            }
        }
        private void SaveTaskToDatabase()
        {
            string inputText = (string)RadioListBox.SelectedItem;
            if (!string.IsNullOrEmpty(inputText))
            {
                string username = ((App)Application.Current).SharedViewModel.LoggedInUsername;
                Todo newTask = new Todo
                {
                    username = username,
                    task = inputText,
                    time = TimeSpan.FromMinutes(25) - timeRemaining
                };

                ((App)Application.Current).SharedViewModel.SaveTaskToDatabase(newTask);
                RadioListBox.Items.Remove(inputText);
                TextInput.Clear();
            }
        }

        private void UpdateUserTasksListBox()
        {
            string username = ((App)Application.Current).SharedViewModel.LoggedInUsername;
            List<string> userTasks = ((App)Application.Current).SharedViewModel.GetUserTasks(username);

            RadioListBox.Items.Clear();
            foreach (string task in userTasks)
            {
                RadioListBox.Items.Add(task);
            }
        }

    }
}
