using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Auth(LoginTextBox.Text.Trim(), PasswordBox.Password);
        }

        public bool Auth(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                var client = Core.Context.Client
                    .Where(c => c.Login == login && c.Password == password)
                    .FirstOrDefault();

                if (client == null)
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    if (PasswordBox != null)
                        PasswordBox.Password = "";
                    return false;
                }

                CurrentUser.ClientId = client.ID;
                CurrentUser.ClientLogin = client.Login;
                CurrentUser.ClientName = client.Name;
                CurrentUser.ClientMail = client.Mail;

                MessageBox.Show($"Добро пожаловать, {client.Name}!",
                    "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);

               
                if (Application.Current != null && Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.UpdateAuthUI();
                }

                
                if (NavigationService != null)
                {
                    NavigationService.GoBack();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void RegisterLinkButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}