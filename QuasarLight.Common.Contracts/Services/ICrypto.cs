namespace QuasarLight.Common.Contracts.Services
{
    public interface ICrypto
    {
        string Hash(string data);
        string EncryptStringAes(string plainText, string sharedSecret);
        string DecryptStringAes(string cipherText, string sharedSecret);
    }
}