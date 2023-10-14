using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Platforms
{
    public class PlatformAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public PlatformAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke.
        /// </summary>
        /// <param name="httpContext">httpContext.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}
