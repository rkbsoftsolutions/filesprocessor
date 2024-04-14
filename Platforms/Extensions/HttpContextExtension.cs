using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Gateway.Extensions
{
    public static class HttpContextExtension
    {

        public static Guid UserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor?.HttpContext.User.Claims.Any(x => x.Type == ClaimTypes.NameIdentifier) ?? false
              ?
             Guid.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value) :
             Guid.Empty;
        }

        public static Guid UserId(this HttpContext httpContext)
        {
            return httpContext.User.Claims.Any(x => x.Type == ClaimTypes.NameIdentifier) 
              ?
             Guid.Parse(httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value) :
             Guid.Empty;
        }

    }
}
