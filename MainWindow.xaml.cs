using System.Windows;

namespace Pr4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Page1());
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page1());
        }

        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page2());
        }

        private void Page3_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page3());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}