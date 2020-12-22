using System.Security.Claims;

namespace ActionFilters.JWTTokenAuthentication
{
    public interface IJWTTokenManager
    {
        bool Authenticate(string user, string pwd);
        string NewToken();
        ClaimsPrincipal VerifyToken(string token);
    }
}