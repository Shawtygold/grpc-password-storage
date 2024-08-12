using System.Security.Cryptography;
using System.Text.Json;

namespace PasswordBoxGrpcServer.Model
{
    internal class JSONReader
    {
        internal static AESConfigStructure ReadAesConfig()
        {
            using (StreamReader streamReader = new("config.json"))
            {
                string json = streamReader.ReadToEnd();
                AESConfigStructure? data = JsonSerializer.Deserialize<AESConfigStructure>(json);

                ArgumentNullException.ThrowIfNull(data);
                return data;
            }
        }
    }

    internal class AESConfigStructure
    {
        internal string Key { get; set; } = null!;
        internal string IV { get; set; } = null!;
        internal string Padding { get; set; } = null!;
        internal string Mode { get; set; } = null!;
        internal string BlockSize { get; set; } = null!;
        internal string KeySize { get; set; } = null!;
    }
}
