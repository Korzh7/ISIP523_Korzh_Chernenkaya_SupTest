using System;

namespace BankAccountNS
{
    /// <summary>
    /// Демонстрационный класс банковского счета, предоставляющий базовые операции
    /// по управлению средствами: пополнение и списание.
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Имя владельца счета (только для чтения).
        /// </summary>
        private readonly string m_customerName;

        /// <summary>
        /// Текущий баланс счета.
        /// </summary>
        private double m_balance;

        /// <summary>
        /// Приватный конструктор по умолчанию, запрещающий создание счета без параметров.
        /// </summary>
        private BankAccount() { }

        /// <summary>
        /// Создаёт новый экземпляр банковского счёта с указанным именем владельца и начальным балансом.
        /// </summary>
        /// <param name="customerName">Имя владельца счета.</param>
        /// <param name="balance">Начальная сумма на счёте.</param>
        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        /// <summary>
        /// Возвращает имя владельца счета.
        /// </summary>
        public string CustomerName
        {
            get { return m_customerName; }
        }

        /// <summary>
        /// Возвращает текущий баланс счета.
        /// </summary>
        public double Balance
        {
            get { return m_balance; }
        }

        /// <summary>
        /// Выполняет списание средств со счета.
        /// </summary>
        /// <param name="amount">Сумма списания.</param>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если сумма списания превышает баланс или отрицательна.</exception>
        public void Debit(double amount)
        {
            if (amount > m_balance)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance -= amount; 
        }

        /// <summary>
        /// Выполняет зачисление средств на счет.
        /// </summary>
        /// <param name="amount">Сумма зачисления.</param>
        /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если сумма зачисления отрицательна.</exception>
        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance += amount;
        }

        /// <summary>
        /// Точка входа в программу. Демонстрирует работу банковского счета.
        /// </summary>
        /// <param name="args">Аргументы командной строки (не используются).</param>
        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Roman Abramovich", 11.99);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
            Console.ReadLine();
        }
    }
}