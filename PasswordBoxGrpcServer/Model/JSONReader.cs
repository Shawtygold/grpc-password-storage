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
        public string Key { get; set; } = null!;
        public string IV { get; set; } = null!;
    }
}
