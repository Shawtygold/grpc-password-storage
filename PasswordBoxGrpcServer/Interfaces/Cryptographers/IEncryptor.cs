namespace PasswordBoxGrpcServer.Interfaces.Cryptographers
{
    public interface IEncryptor
    {
        Task<byte[]> EncryptAsync(string text);
        Task<string> DecryptAsync(byte[] bytes);
    }
}
