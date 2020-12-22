using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActionFilters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionFilters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class SecretController : ControllerBase
    {
        [HttpGet (Name = "Secret")]
        public IActionResult GetSecret()
        {
            return Ok("I have not a secret");
        }
    }
}
