using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp8;

namespace UnitTestProject_Registration
{
    [TestClass]
    public class RegistrationTests
    {
        private RegisterPage page;

        [TestInitialize]
        public void Setup()
        {
            page = new RegisterPage();
        }

        [TestMethod]
        public void RegisterTest_Positive()
        {
            string uniqueLogin = $"testuser_{DateTime.Now.Ticks}";
            string uniqueMail = $"{uniqueLogin}@test.ru";

            bool result = page.Register(
                "Тестовый Пользователь",
                uniqueLogin,
                uniqueMail,
                "Test123",
                "Test123"
            );

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegisterTest_Negative()
        {
            Assert.IsFalse(page.Register("", "login1", "mail@test.ru", "pass123", "pass123"));
            Assert.IsFalse(page.Register("Имя", "", "mail@test.ru", "pass123", "pass123"));
            Assert.IsFalse(page.Register("Имя", "login1", "", "pass123", "pass123"));
            Assert.IsFalse(page.Register("Имя", "login1", "mail@test.ru", "", ""));
            Assert.IsFalse(page.Register("Имя", "login1", "testmail.ru", "pass123", "pass123"));
            Assert.IsFalse(page.Register("Имя", "login1", "test@mailru", "pass123", "pass123"));
            Assert.IsFalse(page.Register("Имя", "login1", "mail@test.ru", "pass123", "wrongpass"));
            Assert.IsFalse(page.Register("Имя", "login1", "mail@test.ru", "12", "12"));
            Assert.IsFalse(page.Register("Имя", "Ivan", "unique_mail@test.ru", "pass123", "pass123"));
            Assert.IsFalse(page.Register("Имя", "unique_login", "ivan@mail.ru", "pass123", "pass123"));
        }
    }
}