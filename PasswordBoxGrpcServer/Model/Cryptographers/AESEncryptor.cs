using PasswordBoxGrpcServer.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PasswordBoxGrpcServer.Model.Cryptographers
{
    internal class AESEncryptor : IEncryptor
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        internal AESEncryptor()
        {
            var config = JSONReader.ReadAesConfig();
            _key = Encoding.Default.GetBytes(config.Key);
            _iv = Encoding.Default.GetBytes(config.IV);
        }

        public async Task<string> DecryptAsync(byte[] encryptedText)
        {
            return await DecryptStringFromBytesAsync_Aes(encryptedText, _key, _iv);
        }

        public async Task<byte[]> EncryptAsync(string text)
        {
            return await EncryptStringToBytesAsync_Aes(text, _key, _iv);
        }

        private static async Task<byte[]> EncryptStringToBytesAsync_Aes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new())
                {
                    using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new(csEncrypt))
                        {
                            await swEncrypt.WriteAsync(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        private static async Task<string> DecryptStringFromBytesAsync_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = await srDecrypt.ReadToEndAsync();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
