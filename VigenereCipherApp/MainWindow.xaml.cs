using System;
using System.Windows;

namespace VigenereCipherApp
{
    public partial class MainWindow : Window
    {
        private VigenereCipher _cipher;

        public MainWindow()
        {
            InitializeComponent();
            _cipher = new VigenereCipher();
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateKey();
                string result = _cipher.Encrypt(txtInput.Text, txtKey.Text);
                txtOutput.Text = result;
                MessageBox.Show("Текст успешно зашифрован!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateKey();
                string result = _cipher.Decrypt(txtInput.Text, txtKey.Text);
                txtOutput.Text = result;
                MessageBox.Show("Текст успешно расшифрован!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSwap_Click(object sender, RoutedEventArgs e)
        {
            string temp = txtInput.Text;
            txtInput.Text = txtOutput.Text;
            txtOutput.Text = temp;
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOutput.Text))
            {
                Clipboard.SetText(txtOutput.Text);
                MessageBox.Show("Результат скопирован в буфер обмена!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "";
            txtKey.Text = "";
            txtOutput.Text = "";
        }

        private void ValidateKey()
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text))
                throw new ArgumentException("Ключ не может быть пустым!");

            foreach (char c in txtKey.Text)
            {
                if (!char.IsLetter(c))
                    throw new ArgumentException("Ключ должен содержать только буквы!");
            }
        }
    }
}