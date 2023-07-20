namespace Collect.io.BL.Auth
{
    public interface IEncrypt
    {
        string HashPassword(string password, string salt);
    }
}
