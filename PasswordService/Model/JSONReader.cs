using Newtonsoft.Json;

namespace PasswordService.Model
{
    public class JSONReader
    {
        public static AESConfigStructure ReadAesConfig(string configPath)
        {
            using (StreamReader streamReader = new(configPath))
            {
                string json = streamReader.ReadToEnd();
                AESConfigStructure? data = JsonConvert.DeserializeObject<AESConfigStructure>(json);

                ArgumentNullException.ThrowIfNull(data);
                return data;
            }
        }
    }

    public class AESConfigStructure
    {
        public string IV { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string Mode { get; set; } = null!;
        public string Padding { get; set; } = null!;
    }
}
