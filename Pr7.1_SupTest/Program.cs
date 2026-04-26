using System;

namespace FibonacciApp
{
    internal class Program
    {
        /// <summary>Главный метод приложения.</summary>
        static void Main(string[] args)
        {
            int result = Fibonacci(5);
            Console.WriteLine($"Результат: {result}");
            Console.ReadKey();
        }

        /// <summary>Вычисляет n-е число Фибоначчи.</summary>
        /// <param name="n">Позиция числа (начиная с 0).</param>
        /// <returns>n-е число Фибоначчи.</returns>
        static int Fibonacci(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            int prev = 0, current = 1, next = 0;

            // ИСПРАВЛЕНО: i <= n вместо i < n
            for (int i = 2; i <= n; i++)
            {
                next = prev + current;
                prev = current;
                current = next;
            }
            return current;
        }
    }
}