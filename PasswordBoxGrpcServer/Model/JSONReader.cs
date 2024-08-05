using System.Text.Json;

namespace PasswordBoxGrpcServer.Model
{
    internal class JSONReader
    {
        internal static AESConfig ReadAesConfig()
        {
            using (StreamReader streamReader = new("config.json"))
            {
                string json = streamReader.ReadToEnd();
                AESConfig? data = JsonSerializer.Deserialize<AESConfig>(json);

                ArgumentNullException.ThrowIfNull(data);

                return data;
            }
        }
    }

    internal class AESConfig
    {
        public string Key { get; set; } = null!;
        public string IV { get; set; } = null!;
    }
}
