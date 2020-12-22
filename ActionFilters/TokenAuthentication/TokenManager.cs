using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFilters.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private readonly List<Token> tokens;
        public TokenManager()
        {
            tokens = new List<Token>();
        }
        public bool Authenticate(string user, string pwd)
        {
            if (!string.IsNullOrEmpty(user) &&
                !string.IsNullOrEmpty(pwd) &&
                user.ToLower() == "admin" && pwd == "password")
                return true;

            return false;
        }

        public Token NewToken()
        {
            Token token = new Token()
            {
                Value = Guid.NewGuid().ToString(),
                ExpiredDate = DateTime.Now.AddHours(1)
            };

            tokens.Add(token);
            return token;
        }

        public bool VerifyToken(string token)
        {
            if (tokens.Any(x => x.Value == token
                    && x.ExpiredDate > DateTime.Now))
                return true;

            return false;
        }
    }
}
