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

                string functionType = GetSelectedFunctionType();
                double result = CalculateFunction2(x, y, functionType);

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

        /// <summary>
        /// Получает выбранный тип функции из RadioButton
        /// </summary>
        /// <returns>Строковое обозначение типа функции</returns>
        private string GetSelectedFunctionType()
        {
            if (rbSh.IsChecked == true)
                return "sh";
            else if (rbX2.IsChecked == true)
                return "x2";
            else if (rbExp.IsChecked == true)
                return "exp";
            else
                return "sh";
        }

        /// <summary>
        /// Вычисляет значение f(x) в зависимости от выбранной функции
        /// </summary>
        /// <param name="x">Значение x</param>
        /// <param name="functionType">Тип функции: "sh", "x2", "exp"</param>
        /// <returns>Значение f(x)</returns>
        public static double CalculateFx(double x, string functionType)
        {
            switch (functionType)
            {
                case "sh":
                    return (Math.Exp(x) - Math.Exp(-x)) / 2;
                case "x2":
                    return x * x;
                case "exp":
                    return Math.Exp(x);
                default:
                    return (Math.Exp(x) - Math.Exp(-x)) / 2;
            }
        }

        /// <summary>
        /// Вычисляет значение второй функции с условиями
        /// a = { (f(x)+y)² - √(f(x)y), xy>0; (f(x)+y)² + √(|f(x)y|), xy<0; (f(x)+y)² + 1, xy=0 }
        /// </summary>
        /// <param name="x">Значение x</param>
        /// <param name="y">Значение y</param>
        /// <param name="functionType">Тип функции f(x): "sh", "x2", "exp"</param>
        /// <returns>Результат вычисления</returns>
        /// <exception cref="ArgumentException">Выбрасывается при недопустимых значениях аргументов</exception>
        public static double CalculateFunction2(double x, double y, string functionType)
        {
            double fx = CalculateFx(x, functionType);
            double xy = x * y;
            double result;

            if (xy > 0)
            {
                if (fx * y < 0)
                {
                    throw new ArgumentException("Под корнем отрицательное число");
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

            return result;
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