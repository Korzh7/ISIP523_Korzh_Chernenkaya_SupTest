using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp8;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private LoginPage page;

        [TestInitialize]
        public void Setup()
        {
            page = new LoginPage();
        }

        [TestMethod]
        public void AuthTest()
        {
            Assert.IsTrue(page.Auth("test", "test"));
            Assert.IsFalse(page.Auth("user1", "12345"));
            Assert.IsFalse(page.Auth("", ""));
            Assert.IsFalse(page.Auth(" ", " "));
        }

        [TestMethod]
        public void AuthTestSuccess()
        {
            Assert.IsTrue(page.Auth("Ivan", "pass123"));
            Assert.IsTrue(page.Auth("Maria", "pass456"));
            Assert.IsTrue(page.Auth("1", "111"));
            Assert.IsTrue(page.Auth("Pany_top", "333"));
            Assert.IsTrue(page.Auth("рр", "1234"));
        }

        [TestMethod]
        public void AuthTestFail()
        {
            Assert.IsFalse(page.Auth("Kar@gmai.com", "6QF1WB"));

            Assert.IsFalse(page.Auth("neexist@mail.ru", "12345"));

            Assert.IsFalse(page.Auth("Ivan", "wrongpass"));

            Assert.IsFalse(page.Auth("", ""));

            Assert.IsFalse(page.Auth("Ivan", ""));

            Assert.IsFalse(page.Auth("", "pass123"));

            Assert.IsFalse(page.Auth("Ivan", "PASS123"));
        }
    }
}