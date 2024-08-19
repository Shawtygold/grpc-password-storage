using PasswordBoxGrpcServer.Interfaces.Cryptographers;
using PasswordBoxGrpcServer.Interfaces.Cryptographers.Configs;
using System.Security.Cryptography;

namespace PasswordBoxGrpcServer.Model.Cryptographers
{
    public class AESEncryptor : IEncryptor
    {
        private readonly IAesConfig _aesConfig;

        public AESEncryptor(IAesConfig config)
        {
            _aesConfig = config;
        }

        public async Task<string> DecryptAsync(string encryptedText)
        {
            byte[] bytes = Convert.FromBase64String(encryptedText);
            return await DecryptStringFromBytesAsync_Aes(bytes, _aesConfig);
        }

        public async Task<string> EncryptAsync(string text)
        {
            return Convert.ToBase64String(await EncryptStringToBytesAsync_Aes(text, _aesConfig));
        }

        private static async Task<byte[]> EncryptStringToBytesAsync_Aes(string plainText, IAesConfig aesConfig)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (aesConfig.Key == null || aesConfig.Key.Length <= 0)
                throw new ArgumentNullException(nameof(aesConfig.Key));
            if (aesConfig.IV == null || aesConfig.IV.Length <= 0)
                throw new ArgumentNullException(nameof(aesConfig.IV));

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesConfig.Key;
                aesAlg.IV = aesConfig.IV;
                aesAlg.Mode = aesConfig.Mode;
                aesAlg.Padding = aesConfig.Padding;

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

        private static async Task<string> DecryptStringFromBytesAsync_Aes(byte[] cipherText, IAesConfig aesConfig)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (aesConfig.Key == null || aesConfig.Key.Length <= 0)
                throw new ArgumentNullException(nameof(aesConfig.Key));
            if (aesConfig.IV == null || aesConfig.IV.Length <= 0)
                throw new ArgumentNullException(nameof(aesConfig.IV));

            string plaintext;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesConfig.Key;
                aesAlg.IV = aesConfig.IV;
                aesAlg.Mode = aesConfig.Mode;
                aesAlg.Padding = aesConfig.Padding;

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
