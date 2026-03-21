using System;
using System.Windows;
using System.Windows.Controls;

namespace Pr4
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtX.Text) ||
                    string.IsNullOrWhiteSpace(txtY.Text) ||
                    string.IsNullOrWhiteSpace(txtZ.Text))
                {
                    txtError.Text = "❌ Ошибка: Заполните все поля ввода!";
                    return;
                }

                if (!double.TryParse(txtX.Text, out double x))
                {
                    txtError.Text = "❌ Ошибка: Введите корректное число для x";
                    return;
                }

                if (!double.TryParse(txtY.Text, out double y))
                {
                    txtError.Text = "❌ Ошибка: Введите корректное число для y";
                    return;
                }

                if (!double.TryParse(txtZ.Text, out double z))
                {
                    txtError.Text = "❌ Ошибка: Введите корректное число для z";
                    return;
                }

                double result = CalculateFunction1(x, y, z);
                txtResult.Text = result.ToString("F6");
                txtError.Text = "✓ Вычисление выполнено успешно!";
                txtError.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (Exception ex)
            {
                txtError.Text = $"❌ Ошибка: {ex.Message}";
                txtError.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        /// <summary>
        /// Вычисляет значение первой функции
        /// Формула: t = (2·cos(x-π/6))/(0.5+sin²y) · (1 + z²/(3-z²/5))
        /// </summary>
        /// <param name="x">Значение x</param>
        /// <param name="y">Значение y</param>
        /// <param name="z">Значение z</param>
        /// <returns>Результат вычисления</returns>
        /// <exception cref="ArgumentException">Выбрасывается при недопустимых значениях аргументов</exception>
        public static double CalculateFunction1(double x, double y, double z)
        {
            double denominator1 = 0.5 + Math.Pow(Math.Sin(y), 2);
            if (Math.Abs(denominator1) < 1e-10)
            {
                throw new ArgumentException("Знаменатель (0.5+sin²y) не может быть равен 0");
            }

            double zSquare = Math.Pow(z, 2);
            if (Math.Abs(3 - zSquare / 5) < 1e-10)
            {
                throw new ArgumentException("z² не должно быть равно 15");
            }

            double part1 = 2 * Math.Cos(x - Math.PI / 6);
            double part2 = denominator1;
            double part3 = 1 + zSquare / (3 - zSquare / 5);

            return (part1 / part2) * part3;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtX.Clear();
            txtY.Clear();
            txtZ.Clear();
            txtResult.Clear();
            txtError.Text = "";
        }
    }
}