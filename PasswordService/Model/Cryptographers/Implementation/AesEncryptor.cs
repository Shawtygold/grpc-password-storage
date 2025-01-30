using AESEncryptionLib;

namespace PasswordService.Model.Cryptographers.Implementation
{
    public class AesEncryptor : IEncryptor
    {
        private readonly IAesConfig _aesConfig;
        private readonly IAes _aes;

        public AesEncryptor(IAes aes, IAesConfig config)
        {
            _aes = aes;
            _aesConfig = config;
        }

        public async Task<string> DecryptAsync(string encryptedText)
        {
            return await _aes.DecryptStringFromBytesAsync_Aes(Convert.FromBase64String(encryptedText), _aesConfig);
        }

        public async Task<string> EncryptAsync(string text)
        {
            return Convert.ToBase64String(await _aes.EncryptStringToBytesAsync_Aes(text, _aesConfig));
        }
    }
}
