namespace PasswordService.Model.Encryption
{
    public interface IEncryptor
    {
        Task<byte[]> EncryptAsync(string text);
        Task<string> DecryptAsync(byte[] encryptedText);
    }
}
