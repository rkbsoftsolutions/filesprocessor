using AuthenticationSvc.IdentityClasses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationSvc.Interface;

namespace AuthenticationSvc.Extensions
{
   public static class IdentityStoreManager
    {
        public static IServiceCollection AddIdentityStoreManager(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUsers, ApplicationRoles>((p) =>
            {
                p.Password = new PasswordOptions
                {
                    RequiredLength = 1,
                    RequiredUniqueChars = 0,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false

                };
                p.SignIn = new SignInOptions { RequireConfirmedEmail = true, RequireConfirmedPhoneNumber = false };
                p.User = new UserOptions { RequireUniqueEmail = true };
                //  Require a confirmed email in order to log in
                p.SignIn.RequireConfirmedEmail = true;

            }).AddRoles<ApplicationRoles>()
             .AddRoleManager<RoleManager<ApplicationRoles>>()
             .AddSignInManager<SignInManager<ApplicationUsers>>()
             .AddUserManager<UserManager<ApplicationUsers>>()
               .AddEntityFrameworkStores<UserPlatfromdbContext>()
               .AddTokenProvider("MyApp", typeof(DataProtectorTokenProvider<ApplicationUsers>));
            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
            //    config.AddPolicy(Policies.User, Policies.UserPolicy());
            //});

            services.AddScoped<ITokenProcessor, JwtTokenProcessor>();
            return services;
        }
    }
}
