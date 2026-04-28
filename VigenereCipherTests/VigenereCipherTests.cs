using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VigenereCipherApp;

namespace VigenereCipherTests
{
    [TestClass]
    public class VigenereCipherTests
    {
        private VigenereCipher _cipher;

        [TestInitialize]
        public void Setup()
        {
            _cipher = new VigenereCipher();
        }

        
        [TestMethod]
        public void Encrypt_EnglishText_ReturnsCiphertext()
        {
            string result = _cipher.Encrypt("HELLO", "KEY");
            Assert.AreEqual("RIJVS", result);  
        }

        [TestMethod]
        public void Decrypt_EnglishCipher_ReturnsPlaintext()
        {
            string result = _cipher.Decrypt("RIJVS", "KEY"); 
            Assert.AreEqual("HELLO", result);
        }

        [TestMethod]
        public void EncryptDecrypt_Roundtrip_ReturnsOriginal()
        {
            string original = "Поддержка и тестирование";
            string encrypted = _cipher.Encrypt(original, "ключ");
            string decrypted = _cipher.Decrypt(encrypted, "ключ");
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void Encrypt_EmptyText_ReturnsEmpty()
        {
            string result = _cipher.Encrypt("", "key");
            Assert.AreEqual("", result);
        }

        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_NullText_ThrowsException()
        {
            _cipher.Encrypt(null, "key");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_NullKey_ThrowsException()
        {
            _cipher.Encrypt("text", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Encrypt_EmptyKey_ThrowsException()
        {
            _cipher.Encrypt("text", "");
        }
    }
}