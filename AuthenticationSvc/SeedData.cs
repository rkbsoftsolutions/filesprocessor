using AuthenticationSvc.IdentityClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationSvc
{
    using Microsoft.AspNetCore.Identity;
    public class SampleData
    {
        public async Task Initialize(IServiceCollection serviceProviderCollection)
        {
            using (var serviceProvider = serviceProviderCollection.BuildServiceProvider())
            {
                var context = serviceProvider.GetService<UserPlatfromdbContext>();
                var roleMgr = serviceProvider.GetService<RoleManager<ApplicationRoles>>();
                UserManager<ApplicationUsers> _userManager = serviceProvider.GetService<UserManager<ApplicationUsers>>();
                string[] roles = new string[] { "User", "Admin" };
                
                foreach (string role in roles)
                {
                        if (!context.Roles.Any(r => r.Name == role))
                        {
                        await roleMgr.CreateAsync(new ApplicationRoles
                        {
                            Id = Guid.NewGuid(),
                            ConcurrencyStamp = Guid.NewGuid().ToString(),
                            Name = role,
                            NormalizedName = role
                        });
                        }
                }


                var user = new ApplicationUsers
                {
                    Email = "admin@admin.com",
                    NormalizedEmail = "admin@admin.COM",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "OWNER",
                    PhoneNumber = "111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };


                if (!context.users.Any(u => u.UserName == user.UserName))
                {


                 var newUser =  await _userManager.CreateAsync(user, "123456").ConfigureAwait(false);

                    if (newUser.Succeeded)
                    {
                        await AssignRoles(serviceProvider, user.Email, roles);
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        private async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUsers> _userManager = services.GetService<UserManager<ApplicationUsers>>();
            ApplicationUsers user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

    }
}
