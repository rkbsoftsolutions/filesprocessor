﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Platforms
{
    public class CustomJwtBearerEvents : JwtBearerEvents
    {

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            // Add the access_token as a claim, as we may actually need it
            var accessToken = context.SecurityToken as JwtSecurityToken;
            //if (Guid.TryParse(accessToken.Id, out Guid sessionId))
            //{
            //    if (await _session.ValidateSessionAsync(sessionId))
            //    {
            //        return;
            //    }
            //}
            throw new SecurityTokenValidationException("Session not valid for provided token.");
        }
    }
    public static class AddPlatformAuthenticationExtension
    {
        public static IServiceCollection AddPlatformAuthentication(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer((c) =>
                {
                    c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["Jwt:ValidIssuer"],
                        ValidAudience = _configuration["Jwt:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]))
                    };
                    //c.Events.on = async context =>
                    //{

                    //};
                    // c.EventsType = typeof(CustomJwtBearerEvents);
                    //c.Events.OnTokenValidated = typeof(CustomJwtBearerEvents)
                });


                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    
                }).AddCookie(options =>
               {
                   options.AccessDeniedPath = new PathString("/Account/Index");
                   options.LoginPath = new PathString("/Account/Index");
                   options.LogoutPath = new PathString("/Account/Index");
                   options.Events.OnSignedIn = (context) =>
                   {
                       context.HttpContext.User = context.Principal;
                       return Task.CompletedTask;
                   };
               });

            return services;
        }
  
    
    }
}
