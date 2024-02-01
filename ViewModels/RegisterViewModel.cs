using System;
using System.Windows;
using System.Windows.Input;
using app.Models;
using app.Command;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using app;
namespace app
{
    public class RegisterViewModel : INotifyPropertyChanged
    {

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private Todo _todo;

        public Todo Todo
        {
            get { return _todo; }
            set
            {
                _todo = value;
                OnPropertyChanged(nameof(Todo));
            }
        }


        public string Password { get; internal set; }

        private bool _isLoginButtonVisible;
        public bool IsLoginButtonVisible
        {
            get { return _isLoginButtonVisible; }
            set
            {
                _isLoginButtonVisible = value;
                OnPropertyChanged(nameof(IsLoginButtonVisible));
            }
        }
        private string _loggedInUsername;
        public string LoggedInUsername
        {
            get { return _loggedInUsername; }
            set
            {
                _loggedInUsername = value;
                OnPropertyChanged(nameof(LoggedInUsername));
            }
        }

        private string _LoggedInUserId;
        public string LoggedInUserId
        {
            get { return _LoggedInUserId; }
            set
            {
                _LoggedInUserId = value;
                OnPropertyChanged(nameof(LoggedInUserId));
            }
        }
        public RegisterViewModel()
        {
            User = new User();
            RegisterCommand = new RelayCommand(Register, CanRegister);
            LoginCommand = new RelayCommand(Login, CanLogin);
            IsLoginButtonVisible = true;
        }
        private bool CanLogin(object parameter)
        {
            return true;
        }
        private void Login(object parameter)
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM users WHERE Username = @Username AND Password = @Password";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", User.Username);
                        cmd.Parameters.AddWithValue("@UserId", User.UserId);
                        cmd.Parameters.AddWithValue("@Password", User.Password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MessageBox.Show("Login Successful by " + User.Username);

                                // Kullanıcının kimliğini al ve kaydet
                                // ((App)Application.Current).SharedViewModel.LoggedInUserId = reader.GetInt32(User.UserId).ToString();
                                ((App)Application.Current).SharedViewModel.LoggedInUsername = User.Username;
                                ((App)Application.Current).SharedViewModel.IsLoginButtonVisible = false;

                                // Ana pencereyi aç
                                Application.Current.MainWindow.Content = new MainWindow();
                            }
                            else
                            {
                                MessageBox.Show("Login Failed");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}");
            }
        }


        private bool CanRegister(object parameter)
        {
            return true;
        }

        private void Register(object parameter)
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO users (Username, Name, Surname, Password) VALUES (@Username, @Name, @Surname, @Password)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", User.Username);
                        cmd.Parameters.AddWithValue("@Name", User.Name);
                        cmd.Parameters.AddWithValue("@Surname", User.Surname);
                        cmd.Parameters.AddWithValue("@Password", User.Password);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kayıt Başarılı! Giriş yaparak çalışmalarınıza başlayabilirsiniz");
                        Application.Current.MainWindow.Content = new LoginPage();

                    }
                }

                User = new User();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred during registration: {ex.Message}");

            }
        }
        public void SaveTaskToDatabase(Todo Todo)
        {
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO todolist (username, task, time) VALUES (@Username, @Task, @Time)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", Todo.username);
                        cmd.Parameters.AddWithValue("@Task", Todo.task);
                        cmd.Parameters.AddWithValue("@Time", Todo.time.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Görev başarıyla veritabanına eklendi");
                    }
                }
            }
            catch (Exception ex)
            {
                var LoggedInUsernameConrol = ((App)Application.Current).SharedViewModel.LoggedInUsername;
                if (LoggedInUsernameConrol != "")
                {
                    MessageBox.Show("Çalışmalarınız kaybolmasın istiyorsanız Lütfen bir kullanıcı giriniz !");
                }
                else
                {
                    MessageBox.Show($"Görev kaydedilirken bir hata oluştu: {ex.Message}");
                }

            }
        }

        public void UpdateTaskToDatabase(Todo Todo)
        {
            try
            {

                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE todolist SET time = @Time WHERE username = @Username AND task = @Task";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))

                    {
                        cmd.Parameters.AddWithValue("@Username", Todo.username);
                        cmd.Parameters.AddWithValue("@Task", Todo.task);
                        cmd.Parameters.AddWithValue("@Time", Todo.time.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Görev başarıyla güncellendi");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Görev güncellenirken bir hata oluştu: {ex.Message}");
            }
        }
        public List<string> GetUserTasks(string username)
        {
            List<string> userTasks = new List<string>();

            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string query = "SELECT task FROM todolist WHERE username = @Username";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string task = reader.GetString("task");
                                userTasks.Add(task);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Görevler alınırken bir hata oluştu: {ex.Message}");
            }

            return userTasks;
        }

        private static MySqlConnection GetConnection()
        {
            const string connectionString = "Server=localhost;Database=wpf_timer;User Id=root;Password=Mysql@123;";
            return new MySqlConnection(connectionString);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}