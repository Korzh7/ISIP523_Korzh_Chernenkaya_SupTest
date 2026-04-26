using System;

namespace LettersApp
{
    /// <summary>Главный класс приложения.</summary>
    class ArrayExample
    {
        /// <summary>Точка входа в приложение.</summary>
        static void Main()
        {
            char[] letters = { 'f', 'r', 'e', 'd', ' ', 's', 'm', 'i', 't', 'h' };
            string name = "";
            int[] a = new int[10];

            for (int i = 0; i < letters.Length; i++)
            {
                name += letters[i];
                a[i] = i + 1;
                SendMessage(name, a[i]);
            }
            Console.ReadKey();
        }

        /// <summary>Отправляет приветственное сообщение.</summary>
        /// <param name="name">Имя получателя.</param>
        /// <param name="msg">Число для подсчёта.</param>
        static void SendMessage(string name, int msg)
        {
            Console.WriteLine("Hello, " + name + "! Count to " + msg);
        }
    }
}