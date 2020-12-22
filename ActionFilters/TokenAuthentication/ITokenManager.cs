namespace ActionFilters.TokenAuthentication
{
    public interface ITokenManager
    {
        bool Authenticate(string user, string pwd);
        Token NewToken();
        bool VerifyToken(string token);
    }
}