using AESEncryptionLib;
using System.Security.Cryptography;

namespace PasswordService.Infrastructure.Security
{
    public class AesEncryptionConfig : IAesConfig
    {
        public byte[] Key { get; set; } = null!;
        public byte[] IV { get; set; } = null!;
        public PaddingMode PaddingMode { get; set; }
        public CipherMode CipherMode { get; set; }

        public AesEncryptionConfig(byte[] key, byte[] iv, PaddingMode paddingMode, CipherMode cipherMode)
        {
            Key = key;
            IV = iv;
            PaddingMode = paddingMode;
            CipherMode = cipherMode;
        }
    }
}
