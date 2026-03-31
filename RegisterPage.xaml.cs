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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            string login = LoginTextBox.Text.Trim();
            string mail = MailTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(mail) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все обязательные поля (Имя, Логин, Почта, Пароль)!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!mail.Contains("@") || !mail.Contains("."))
            {
                MessageBox.Show("Введите корректный email адрес!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                ConfirmPasswordBox.Password = "";
                return;
            }

            if (password.Length < 3)
            {
                MessageBox.Show("Пароль должен содержать минимум 3 символа!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           
               
                var existingLogin = Core.Context.Client
                    .Where(c => c.Login == login)
                    .FirstOrDefault();

                if (existingLogin != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var existingMail = Core.Context.Client
                    .Where(c => c.Mail == mail)
                    .FirstOrDefault();

                if (existingMail != null)
                {
                    MessageBox.Show("Пользователь с такой почтой уже зарегистрирован!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                var newClient = new Client
                {
                    Name = name,
                    Login = login,
                    Mail = mail,
                    Password = password
                };

                Core.Context.Client.Add(newClient);
                Core.Context.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти в систему.",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                
                NavigationService.GoBack();
            
            
        }

        private void LoginLinkButton_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.GoBack();
        }
    }
}