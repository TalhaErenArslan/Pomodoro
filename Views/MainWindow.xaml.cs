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
        private string selectedTask;

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.IsChecked == true)
            {
                selectedTask = radioButton.Content.ToString();
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
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRunning)
            {
                timer.Stop();
                isTimerRunning = false;
                ellipsee.Stroke = Brushes.DarkRed;
                SaveTaskToDatabase();
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

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Content = new DetailPage();
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
                    time = TimeSpan.FromMinutes(25)
                };

                ((App)Application.Current).SharedViewModel.SaveTaskToDatabase(newTask);
                RadioListBox.Items.Add(inputText);
                TextInput.Clear();
            }
        }
        private void SaveTaskToDatabase()
        {
            if (!string.IsNullOrEmpty(selectedTask))
            {
                string username = ((App)Application.Current).SharedViewModel.LoggedInUsername;
                Todo newTask = new Todo
                {
                    username = username,
                    task = selectedTask,
                    time = timeRemaining
                };
                ((App)Application.Current).SharedViewModel.UpdateTaskToDatabase(newTask);
            }
        }


        private void UpdateUserTasksListBox()
        {
            string username = ((App)Application.Current).SharedViewModel.LoggedInUsername;
            List<(string Task, TimeSpan Time)> userTasks = ((App)Application.Current).SharedViewModel.GetUserTasks(username);
            RadioListBox.Items.Clear();
            foreach (var taskWithTime in userTasks)
            {
                string task = taskWithTime.Task;
                RadioListBox.Items.Add(task);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedTask))
            {
                string username = ((App)Application.Current).SharedViewModel.LoggedInUsername;
                ((App)Application.Current).SharedViewModel.DeleteTaskFromDatabase(username, selectedTask);
                RadioListBox.Items.Remove(selectedTask);
            }
        }


    }
}