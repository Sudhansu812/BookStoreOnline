using BookSharingOnlineApi.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BookSharingOnlineApi.Services
{
    public class EncryptionService : IEncryptionService
    {
        // Encryption key: has to be same for both encrypt and decrypt
        private readonly string encryptionKey = "sblw-3hn8-sqoy19";

        public string Encrypt(string input)
        {
            byte[] initVector = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider
            {
                Key = UTF8Encoding.UTF8.GetBytes(encryptionKey),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cryptoTransform = cryptoServiceProvider.CreateEncryptor();
            byte[] resultArray = cryptoTransform.TransformFinalBlock(initVector, 0, initVector.Length);
            cryptoServiceProvider.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string input)
        {
            byte[] initVector = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider
            {
                Key = UTF8Encoding.UTF8.GetBytes(encryptionKey),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = cryptoServiceProvider.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(initVector, 0, initVector.Length);
            cryptoServiceProvider.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}