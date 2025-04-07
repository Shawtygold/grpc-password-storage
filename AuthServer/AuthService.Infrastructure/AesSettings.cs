namespace AuthService.Infrastructure
{
    public class AesSettings
    {
        public const string SectionName = "AesSettings";
        public string Key { get; set; } = string.Empty;
        public string IV { get; set; } = string.Empty;
        public string CipherMode { get; set; } = string.Empty;
        public string PaddingMode { get; set; } = string.Empty;
    }
}
