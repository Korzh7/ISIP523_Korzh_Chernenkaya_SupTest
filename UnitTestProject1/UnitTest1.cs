using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pr4;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int res = 2 + 2;
            Assert.AreEqual(res, 4);
            Assert.AreNotEqual(res, 5);
            Assert.IsFalse(res > 5);
            Assert.IsTrue(res < 5);
        }

        /// <summary>
        /// Тест для первой функции
        /// </summary>
        [TestMethod]
        public void TestFunction1()
        {
            double result = Page1.CalculateFunction1(0, 0, 1);
            Assert.AreEqual(4.702, result, 0.001);
        }

        /// <summary>
        /// Тест для второй функции
        /// </summary>
        [TestMethod]
        public void TestFunction2()
        {
            double result = Page2.CalculateFunction2(1, 2, "sh");
            Assert.AreEqual(8.548, result, 0.001);
        }

        /// <summary>
        /// Тест для третьей функции
        /// </summary>
        [TestMethod]
        public void TestFunction3()
        {
            double result = Page3.CalculateFunction3(2, 3, 4, 1);
            Assert.AreEqual(-0.831, result, 0.001);
        }
    }
}