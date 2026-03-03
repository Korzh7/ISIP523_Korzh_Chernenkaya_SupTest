using System;
using System.Windows;
using System.Windows.Controls;

namespace Pr4
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void btnCalculate2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtX2.Text) ||
                    string.IsNullOrWhiteSpace(txtY2.Text))
                {
                    txtError2.Text = "❌ Ошибка: Заполните все поля ввода!";
                    return;
                }

                if (!double.TryParse(txtX2.Text, out double x))
                {
                    txtError2.Text = "❌ Ошибка: Введите корректное число для x";
                    return;
                }

                if (!double.TryParse(txtY2.Text, out double y))
                {
                    txtError2.Text = "❌ Ошибка: Введите корректное число для y";
                    return;
                }

                double fx = 0;
                if (rbSh.IsChecked == true)
                    fx = (Math.Exp(x) - Math.Exp(-x)) / 2;
                else if (rbX2.IsChecked == true)
                    fx = x * x;
                else if (rbExp.IsChecked == true)
                    fx = Math.Exp(x);

                double xy = x * y;
                double result;

                if (xy > 0)
                {
                    if (fx * y < 0)
                    {
                        txtError2.Text = "❌ Ошибка: Под корнем отрицательное число";
                        return;
                    }
                    result = Math.Pow(fx + y, 2) - Math.Sqrt(fx * y);
                }
                else if (xy < 0)
                {
                    result = Math.Pow(fx + y, 2) + Math.Sqrt(Math.Abs(fx * y));
                }
                else
                {
                    result = Math.Pow(fx + y, 2) + 1;
                }

                txtResult2.Text = result.ToString("F6");
                txtError2.Text = "✓ Вычисление выполнено успешно!";
                txtError2.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (Exception ex)
            {
                txtError2.Text = $"❌ Ошибка: {ex.Message}";
                txtError2.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void btnClear2_Click(object sender, RoutedEventArgs e)
        {
            txtX2.Clear();
            txtY2.Clear();
            txtResult2.Clear();
            txtError2.Text = "";
            rbSh.IsChecked = true;
        }
    }
}