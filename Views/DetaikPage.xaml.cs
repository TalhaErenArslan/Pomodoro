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
    public partial class DetailPage : Page
    {
        public DetailPage()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).SharedViewModel;
            UpdateUserTasksListBox();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Content = new MainWindow();
            this.Content = frame;
        }

        private void UpdateUserTasksListBox()
        {
            string username = ((App)Application.Current).SharedViewModel.LoggedInUsername;
            List<(string Task, TimeSpan Time)> userTasks = ((App)Application.Current).SharedViewModel.GetUserTasks(username);

            RadioListBox.Items.Clear();
            foreach (var taskWithTime in userTasks)
            {
                string task = taskWithTime.Task;
                TimeSpan time = taskWithTime.Time;
                string displayText = $"{task}   |   {time.ToString(@"mm\:ss")}"; 

                RadioListBox.Items.Add(displayText);
            }
        }

    }
}

