using System;
using System.Text;

namespace VigenereCipherApp
{
    public class VigenereCipher
    {
        private const string EnglishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string RussianAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public string Encrypt(string text, string key)
        {
            ValidateInputs(text, key);
            return ProcessText(text, key, true);
        }

        public string Decrypt(string text, string key)
        {
            ValidateInputs(text, key);
            return ProcessText(text, key, false);
        }

        private void ValidateInputs(string text, string key)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Ключ не может быть пустым", nameof(key));
        }

        private string ProcessText(string text, string key, bool encrypt)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var result = new StringBuilder();
            int keyIndex = 0;

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    string alphabet = GetAlphabet(c);
                    bool isUpper = char.IsUpper(c);
                    int shift = GetShift(key[keyIndex % key.Length], alphabet);
                    int pos = GetPosition(c, alphabet);

                    int newPos = encrypt
                        ? (pos + shift) % alphabet.Length
                        : (pos - shift + alphabet.Length) % alphabet.Length;

                    char newChar = alphabet[newPos];
                    result.Append(isUpper ? newChar : char.ToLower(newChar));
                    keyIndex++;
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        private string GetAlphabet(char c)
        {
            char upper = char.ToUpper(c);
            if (upper >= 'A' && upper <= 'Z')
                return EnglishAlphabet;
            return RussianAlphabet;
        }

        private int GetShift(char keyChar, string alphabet)
        {
            char upper = char.ToUpper(keyChar);
            int index = alphabet.IndexOf(upper);
            return index >= 0 ? index : 0;
        }

        private int GetPosition(char c, string alphabet)
        {
            return alphabet.IndexOf(char.ToUpper(c));
        }
    }
}