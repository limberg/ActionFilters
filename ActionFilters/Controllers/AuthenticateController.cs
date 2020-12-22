using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActionFilters.JWTTokenAuthentication;
using ActionFilters.TokenAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionFilters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenManager tokenManager;
        private readonly IJWTTokenManager jwtTokenManager;
        public AuthenticateController(ITokenManager tokenManager, IJWTTokenManager jwtTokenManager)
        {
            this.tokenManager = tokenManager;
            this.jwtTokenManager = jwtTokenManager;
        }

        [HttpGet]
        public IActionResult GetToken(string user, string pwd)
        {
            if (tokenManager.Authenticate(user, pwd))
                return Ok(new { Token = tokenManager.NewToken() });
            else
                return Unauthorized();
        }

        [HttpGet(Name ="GetTokenJWT")]
        [Route("tokenJWT")]
        public IActionResult GetTokenJWT(string user, string pwd)
        {
            if (jwtTokenManager.Authenticate(user, pwd))
                return Ok(new { Token = jwtTokenManager.NewToken() });
            else
                return Unauthorized();
        }
    }
}
