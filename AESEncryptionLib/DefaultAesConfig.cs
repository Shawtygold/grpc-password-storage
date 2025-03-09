using System.Security.Cryptography;

namespace AESEncryptionLib
{
    public class DefaultAesConfig : IAesConfig
    {
        private const PaddingMode DefafultPadding = PaddingMode.PKCS7;
        private const CipherMode DefaultMode = CipherMode.CBC;

        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public PaddingMode PaddingMode { get; set; }
        public CipherMode CipherMode { get; set; }

        public DefaultAesConfig()
        {
            Key = RandomNumberGenerator.GetBytes(16);
            IV = RandomNumberGenerator.GetBytes(16);
            PaddingMode = DefafultPadding;
            CipherMode = DefaultMode;
        }
    }
}
