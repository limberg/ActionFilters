using ActionFilters.JWTTokenAuthentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFilters.ActionFilters
{
    public class JWTTokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool result = true;

            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var JWTTokenValue))
            {
                context.ModelState.AddModelError("Unauthorized", "Authorization value false");
                result = false;
            }

            if (result)
            {
                try
                {
                    IJWTTokenManager jwtTokenManger = context.HttpContext.RequestServices.GetRequiredService<IJWTTokenManager>();
                    var claimPrinciple = jwtTokenManger.VerifyToken(JWTTokenValue);
                }
                catch(Exception ex)
                {
                    context.ModelState.AddModelError("Unathorized", ex.Message);
                    result = false;
                }
            }

            if (!result)
            {
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}
