using AESEncryptionLib;
using AuthService.Application.Abstractions.Encryption;

namespace AuthService.Infrastructure.Security
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

        public async Task<byte[]> EncryptAsync(string value)
        {
            return await _aes.EncryptStringToBytesAsync_Aes(value, _aesConfig);
        }

        public async Task<string> DecryptAsync(byte[] encryptedValue)
        {
            return await _aes.DecryptStringFromBytesAsync_Aes(encryptedValue, _aesConfig);
        }
    }
}
