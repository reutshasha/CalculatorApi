using BL.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CalculatorApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authManager = context.HttpContext.RequestServices.GetService<IAuthManager>();
            var logger = context.HttpContext.RequestServices.GetService<ILogger<CustomAuthorizeAttribute>>();

            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var isValid = authManager.ValidateToken(token).Result;

            if (!isValid)
            {
                logger?.LogWarning("Unauthorized request with invalid token.");
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
