using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AuthenticateController(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }

        [HttpGet]
        public IActionResult GetToken(string user, string pwd)
        {
            if (tokenManager.Authenticate(user, pwd))
                return Ok(new { Token = tokenManager.NewToken() });
            else
                return Unauthorized();
        }
    }
}
