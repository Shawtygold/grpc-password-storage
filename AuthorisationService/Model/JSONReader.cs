using Newtonsoft.Json;

namespace AuthService.Model
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
        public string IV { get; set; }
        public string Key { get; set; }
        public string Mode { get; set; }
        public string Padding { get; set; }
    }
}
