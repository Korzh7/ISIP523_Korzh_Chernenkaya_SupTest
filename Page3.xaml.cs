using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Pr4
{
    public partial class Page3 : Page
    {
        public SeriesCollection SeriesCollection { get; set; }

        public Page3()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "y = (0.01·b·c)/x + cos(√(a³·x))",
                    Values = new ChartValues<double>(),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10,
                    StrokeThickness = 2
                }
            };

            chart1.Series = SeriesCollection;
        }

        private void btnCalculate3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtA.Text) ||
                    string.IsNullOrWhiteSpace(txtB.Text) ||
                    string.IsNullOrWhiteSpace(txtC.Text) ||
                    string.IsNullOrWhiteSpace(txtX0.Text) ||
                    string.IsNullOrWhiteSpace(txtXk.Text) ||
                    string.IsNullOrWhiteSpace(txtDx.Text))
                {
                    txtError3.Text = "❌ Ошибка: Заполните все поля ввода!";
                    return;
                }

                if (!double.TryParse(txtA.Text, out double a) ||
                    !double.TryParse(txtB.Text, out double b) ||
                    !double.TryParse(txtC.Text, out double c) ||
                    !double.TryParse(txtX0.Text, out double x0) ||
                    !double.TryParse(txtXk.Text, out double xk) ||
                    !double.TryParse(txtDx.Text, out double dx))
                {
                    txtError3.Text = "❌ Ошибка: Введите корректные числа";
                    return;
                }

                if (dx <= 0)
                {
                    txtError3.Text = "❌ Ошибка: Шаг dx должен быть положительным";
                    return;
                }

                if (x0 > xk)
                {
                    txtError3.Text = "❌ Ошибка: x0 должно быть меньше или равно xk";
                    return;
                }

                SeriesCollection[0].Values.Clear();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("x\t\ty");
                sb.AppendLine("------------------");

                for (double x = x0; x <= xk + 1e-10; x += dx)
                {
                    double y = CalculateFunction3(a, b, c, x);

                    SeriesCollection[0].Values.Add(y);
                    sb.AppendLine($"{x:F3}\t{y:F6}");
                }

                txtResults.Text = sb.ToString();
                txtError3.Text = "✓ Табуляция выполнена успешно!";
                txtError3.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (Exception ex)
            {
                txtError3.Text = $"❌ Ошибка: {ex.Message}";
                txtError3.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        /// <summary>
        /// Вычисляет значение третьей функции
        /// Формула: y = (10⁻²·b·c)/x + cos(√(a³·x))
        /// </summary>
        /// <param name="a">Значение a</param>
        /// <param name="b">Значение b</param>
        /// <param name="c">Значение c</param>
        /// <param name="x">Значение x</param>
        /// <returns>Результат вычисления</returns>
        /// <exception cref="ArgumentException">Выбрасывается при недопустимых значениях аргументов</exception>
        public static double CalculateFunction3(double a, double b, double c, double x)
        {
            if (Math.Abs(x) < 1e-10)
            {
                throw new ArgumentException("x не может быть равен 0");
            }

            double underRoot = Math.Pow(a, 3) * x;
            if (underRoot < 0)
            {
                throw new ArgumentException($"Под корнем отрицательное число при x={x}");
            }

            return (0.01 * b * c) / x + Math.Cos(Math.Sqrt(underRoot));
        }

        private void btnClear3_Click(object sender, RoutedEventArgs e)
        {
            txtA.Text = "2";
            txtB.Text = "3";
            txtC.Text = "4";
            txtX0.Text = "0.5";
            txtXk.Text = "2.5";
            txtDx.Text = "0.5";

            txtResults.Clear();
            txtError3.Text = "";
            SeriesCollection[0].Values.Clear();
        }
    }
}