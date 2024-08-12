using System.Security.Cryptography;

namespace PasswordBoxGrpcServer.Interfaces.Cryptographers.Configs
{
    public interface IAesConfig
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public PaddingMode Padding { get; set; }
        public CipherMode Mode { get; set; }
        public int BlockSize { get; set; }
        public int KeySize { get; set; }
    }
}
