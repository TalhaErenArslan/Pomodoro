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

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        public string Password { get; internal set; }
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
        public RegisterViewModel()
        {
            User = new User();
            RegisterCommand = new RelayCommand(Register, CanRegister);
            LoginCommand = new RelayCommand(Login, CanLogin);
        }
        private bool CanLogin(object parameter)
        {
            // Implement your logic for when the login command can be executed
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
                        cmd.Parameters.AddWithValue("@Password", User.Password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // User found, you can handle the successful login here
                                MessageBox.Show("Login Successful by " + User.Username);
                                LoggedInUsername = User.Username;
                                 MainWindow MainWindow = new MainWindow((DataContext as RegisterViewModel)?.LoggedInUsername);
                                frame.Content = new MainWindow() { DataContext = RegisterViewModel };

                            }
                            else
                            {
                                // User not found or incorrect credentials
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
                        MessageBox.Show("Kayıt Başarılı");

                    }
                }

                User = new User();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during registration: {ex.Message}");

            }
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
