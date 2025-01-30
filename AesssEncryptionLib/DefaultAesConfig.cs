using AESEncryptionLib.Interfaces;
using System.Security.Cryptography;

namespace AESEncryptionLib.Model
{
    public class DefaultAesConfig : IAesConfig
    {
        private const PaddingMode DefafultPadding = PaddingMode.PKCS7;
        private const CipherMode DefaultMode = CipherMode.CBC;

        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public PaddingMode Padding { get; set; }
        public CipherMode Mode { get; set; }

        public DefaultAesConfig()
        {
            Key = RandomNumberGenerator.GetBytes(16);
            IV = RandomNumberGenerator.GetBytes(16);
            Padding = DefafultPadding;
            Mode = DefaultMode;
        }
    }
}
