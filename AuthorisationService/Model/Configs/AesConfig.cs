using AESEncryptionLib;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Model.Configs
{
    public class AesConfig : IAesConfig
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public PaddingMode Padding { get; set; }
        public CipherMode Mode { get; set; }

        public AesConfig()
        {
            var config = JSONReader.ReadAesConfig("config.json");

            Key = Encoding.Default.GetBytes(config.Key);
            IV = Encoding.Default.GetBytes(config.IV);
            Padding = (PaddingMode)Convert.ToInt32(config.Padding);
            Mode = (CipherMode)Convert.ToInt32(config.Mode);
        }
    }
}
