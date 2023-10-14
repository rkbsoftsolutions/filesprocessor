using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;


namespace Platforms.MIddleware
{
    public static class PlatformAuthenticationExtension
    {
        public static IApplicationBuilder UsePlatformAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<PlatformAuthenticationMiddleware>();
            return app;
        }
    }
}
