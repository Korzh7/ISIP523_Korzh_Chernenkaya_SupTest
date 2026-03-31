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
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password;

           
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                
                var client = Core.Context.Client
                    .Where(c => c.Login == login && c.Password == password)
                    .FirstOrDefault();

                if (client != null)
                {
                    
                    CurrentUser.ClientId = client.ID;
                    CurrentUser.ClientLogin = client.Login;
                    CurrentUser.ClientName = client.Name;
                    CurrentUser.ClientMail = client.Mail;

                    MessageBox.Show($"Добро пожаловать, {client.Name}!",
                        "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);

                   
                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    {
                        mainWindow.UpdateAuthUI();
                    }

                    
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    PasswordBox.Password = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterLinkButton_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new RegisterPage());
        }
    }
}