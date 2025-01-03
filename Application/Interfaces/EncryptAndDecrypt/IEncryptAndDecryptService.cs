namespace Application.Interfaces.EncryptAndDecrypt
{
    public interface IEncryptAndDecryptService
    {
        string Decrypt(string encryptedData);
        string Encrypt(string data);
    }
}