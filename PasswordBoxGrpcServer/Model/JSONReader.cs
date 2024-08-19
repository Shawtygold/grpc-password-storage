using Newtonsoft.Json;

namespace PasswordBoxGrpcServer.Model
{
    public class JSONReader
    {
        public static AESConfigStructure ReadAesConfig()
        {
            using (StreamReader streamReader = new("config.json"))
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
