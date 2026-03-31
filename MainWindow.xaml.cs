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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page _mainPage;

        public MainWindow()
        {
            InitializeComponent();

           
            _mainPage = new MainPage();

           
            MainFrame.Navigate(_mainPage);

           
            UpdateAuthUI();
        }

        
        public void UpdateAuthUI()
        {
            if (CurrentUser.IsAuthenticated)
            {
                UserInfoTextBlock.Text = CurrentUser.ClientName;
                AuthButton.Visibility = Visibility.Collapsed;
                ProfileButton.Visibility = Visibility.Visible;
                LogoutButton.Visibility = Visibility.Visible;
            }
            else
            {
                UserInfoTextBlock.Text = "Гость";
                AuthButton.Visibility = Visibility.Visible;
                ProfileButton.Visibility = Visibility.Collapsed;
                LogoutButton.Visibility = Visibility.Collapsed;
            }
        }

       
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            
            if (e.Content is Page page)
            {
                PageTitleTextBlock.Text = page.Title;
            }

          
            BackButton.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

      
       
        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Navigate(new LoginPage());
            PageTitleTextBlock.Text = "Вход в систему";
            BackButton.Visibility = Visibility.Visible;
        }

        
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
           
            MainFrame.Navigate(new ProfilePage());
            PageTitleTextBlock.Text = "Личный кабинет";
            BackButton.Visibility = Visibility.Visible;
        }

       
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?",
                "Выход из системы", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                CurrentUser.Clear();
                UpdateAuthUI();

               
                MainFrame.Navigate(_mainPage);
                PageTitleTextBlock.Text = "Главная страница";
                BackButton.Visibility = Visibility.Collapsed;

                MessageBox.Show("Вы успешно вышли из системы", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}