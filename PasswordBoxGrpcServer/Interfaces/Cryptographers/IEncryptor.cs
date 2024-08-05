namespace PasswordBoxGrpcServer.Interfaces
{
    public interface IEncryptor
    {
        Task<byte[]> EncryptAsync(string text);
        Task<string> DecryptAsync(byte[] bytes);
    }
}
