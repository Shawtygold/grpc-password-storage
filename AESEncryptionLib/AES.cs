using System.Security.Cryptography;

namespace AESEncryptionLib
{
    public class AES : IAes
    {
        public async Task<byte[]> EncryptStringToBytesAsync_Aes(string plainText, IAesConfig aesConfig)
        {
            if (plainText == null || plainText.Length == 0)
                throw new ArgumentNullException(nameof(plainText));
            if (aesConfig.Key == null || aesConfig.Key.Length == 0)
                throw new ArgumentNullException(nameof(aesConfig.Key));
            if (aesConfig.IV == null || aesConfig.IV.Length == 0)
                throw new ArgumentNullException(nameof(aesConfig.IV));

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesConfig.Key;
                aesAlg.IV = aesConfig.IV;
                aesAlg.Mode = aesConfig.CipherMode;
                aesAlg.Padding = aesConfig.PaddingMode;

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

        public async Task<string> DecryptStringFromBytesAsync_Aes(byte[] cipherText, IAesConfig aesConfig)
        {
            if (cipherText == null || cipherText.Length == 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (aesConfig.Key == null || aesConfig.Key.Length == 0)
                throw new ArgumentNullException(nameof(aesConfig.Key));
            if (aesConfig.IV == null || aesConfig.IV.Length == 0)
                throw new ArgumentNullException(nameof(aesConfig.IV));

            string plaintext;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aesConfig.Key;
                aesAlg.IV = aesConfig.IV;
                aesAlg.Mode = aesConfig.CipherMode;
                aesAlg.Padding = aesConfig.PaddingMode;

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
