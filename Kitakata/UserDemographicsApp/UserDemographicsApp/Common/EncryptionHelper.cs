using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UserDemographicsApp.Common
{
    public static class EncryptionHelper
    {
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("DB12E80E3103441BAB34D2D533ED96F1"); // Your 32-byte key

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length); // Write IV to the beginning of the stream

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;

                using (MemoryStream ms = new MemoryStream(cipherBytes))
                {
                    byte[] iv = new byte[aes.BlockSize / 8];
                    ms.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] plainBytes = new byte[cipherBytes.Length - iv.Length];
                        int decryptedCount = cs.Read(plainBytes, 0, plainBytes.Length);

                        return Encoding.UTF8.GetString(plainBytes, 0, decryptedCount);
                    }
                }
            }
        }
    }
}
