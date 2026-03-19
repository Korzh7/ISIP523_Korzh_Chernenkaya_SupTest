using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace BankTests
{
    /// <summary>
    /// Класс для тестирования методов класса BankAccount
    /// </summary>
    [TestClass]
    public class BankAccountTests
    {
        /// <summary>
        /// Тест проверяет корректность списания средств при допустимой сумме
        /// </summary>
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange - подготовка данных для тестирования
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act - выполнение тестируемого действия
            account.Debit(debitAmount);

            // Assert - проверка результата
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        /// <summary>
        /// Тест проверяет, что метод Debit выбрасывает исключение 
        /// ArgumentOutOfRangeException при отрицательной сумме списания
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
        }

        /// <summary>
        /// Тест проверяет, что метод Debit выбрасывает исключение 
        /// ArgumentOutOfRangeException при сумме списания превышающей баланс
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
        }

        /// <summary>
        /// Тест проверяет корректность зачисления средств при допустимой сумме
        /// </summary>
        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = 5.77;
            double expected = 17.76;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly");
        }

        /// <summary>
        /// Тест проверяет, что метод Credit выбрасывает исключение 
        /// ArgumentOutOfRangeException при отрицательной сумме зачисления
        /// </summary>
        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = -5.77;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Credit(creditAmount));
        }

        /// <summary>
        /// Тест проверяет, что метод Credit корректно обрабатывает нулевую сумму зачисления
        /// </summary>
        [TestMethod]
        public void Credit_WithZeroAmount_DoesNotChangeBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = 0.0;
            double expected = 11.99;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Balance should not change with zero credit");
        }

        /// <summary>
        /// Тест проверяет, что метод Credit корректно обрабатывает несколько последовательных зачислений
        /// </summary>
        [TestMethod]
        public void Credit_MultipleCredits_UpdatesBalanceCorrectly()
        {
            // Arrange
            double beginningBalance = 11.99;
            double expected = 11.99 + 5.77 + 10.00 + 3.33;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act
            account.Credit(5.77);
            account.Credit(10.00);
            account.Credit(3.33);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Multiple credits not calculated correctly");
        }

        /// <summary>
        /// Тест проверяет, что метод Credit выбрасывает исключение 
        /// ArgumentOutOfRangeException при очень большой отрицательной сумме
        /// </summary>
        [TestMethod]
        public void Credit_WhenAmountIsHugeNegative_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double creditAmount = -1000000.00;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Credit(creditAmount));
        }
    }
}