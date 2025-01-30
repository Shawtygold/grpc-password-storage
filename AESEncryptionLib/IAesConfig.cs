using System.Security.Cryptography;

namespace AESEncryptionLib
{
    public interface IAesConfig
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public PaddingMode Padding { get; set; }
        public CipherMode Mode { get; set; }
    }
}
