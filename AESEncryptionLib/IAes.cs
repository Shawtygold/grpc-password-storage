namespace AESEncryptionLib
{
    public interface IAes
    {
        Task<byte[]> EncryptStringToBytesAsync_Aes(string plainText, IAesConfig aesConfig);
        Task<string> DecryptStringFromBytesAsync_Aes(byte[] cipherText, IAesConfig aesConfig);
    }
}
