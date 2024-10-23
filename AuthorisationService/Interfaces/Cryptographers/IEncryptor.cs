namespace AuthorisationService.Interfaces.Cryptographers
{
    public interface IEncryptor
    {
        Task<string> EncryptAsync(string text);
        Task<string> DecryptAsync(string encryptedText);
    }
}
