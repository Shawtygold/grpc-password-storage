using PasswordBoxGrpcServer.Interfaces.Cryptographers.Configs;
using System.Security.Cryptography;
using System.Text;

namespace PasswordBoxGrpcServer.Model.Cryptographers.Configs
{
    public class AesConfig : IAesConfig
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public PaddingMode Padding { get; set; }
        public CipherMode Mode { get; set; }
        public int BlockSize { get; set; }
        public int KeySize { get; set; }

        public AesConfig()
        {
            var config = JSONReader.ReadAesConfig();

            Key = Encoding.Default.GetBytes(config.Key);
            IV = Encoding.Default.GetBytes(config.IV);
            Padding = (PaddingMode)Convert.ToInt32(config.Padding);
            Mode = (CipherMode)Convert.ToInt32(config.Mode);
            BlockSize = Convert.ToInt32(config.BlockSize);
            KeySize = Convert.ToInt32(config.KeySize);
        }
    }
}
