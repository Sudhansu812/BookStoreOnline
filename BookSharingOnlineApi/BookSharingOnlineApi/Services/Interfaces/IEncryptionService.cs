namespace BookSharingOnlineApi.Services.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string input);

        string Decrypt(string input);
    }
}