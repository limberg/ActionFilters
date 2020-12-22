using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActionFilters.TokenAuthentication;
using Microsoft.Extensions.DependencyInjection;

namespace ActionFilters.ActionFilters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var tokenManager = context.HttpContext.RequestServices.GetService(typeof(ITokenManager)) as TokenManager;
            var tokenManager = context.HttpContext.RequestServices.GetRequiredService<ITokenManager>();


            bool result = true ;
            if(!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var potentialToken))
            {
                result = false;
            }

            if (result && !tokenManager.VerifyToken(potentialToken))
            {
                result = false;
            }

            if (!result)
            {
                context.ModelState.AddModelError("Unauthorized", "User not Authorized.");
                context.Result = new UnauthorizedObjectResult(context.ModelState);

            }
        }
    }
}
