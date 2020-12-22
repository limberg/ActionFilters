using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ActionFilters.JWTTokenAuthentication
{
    public class JWTTokenManager : IJWTTokenManager
    {

        private JwtSecurityTokenHandler tokenHandler;
        private byte[] secretKey;
        public JWTTokenManager()
        {
            tokenHandler = new JwtSecurityTokenHandler();
            secretKey = Encoding.ASCII.GetBytes("kkkkkkkkkkkkkkkkkkkkkkkkkkkkkk"); //30 k's -> mus aus config datei kommen
        }
        public bool Authenticate(string user, string pwd)
        {
            if (!string.IsNullOrEmpty(user) &&
                !string.IsNullOrEmpty(pwd) &&
                user.ToLower() == "admin" && pwd == "password")
                return true;

            return false;
        }

        public string NewToken()
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "Limberg Rivera") }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            string tokenJWT = tokenHandler.WriteToken(securityToken);
            return tokenJWT;
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            ClaimsPrincipal claims = tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken
                );

            return claims;
        }
    }
}

