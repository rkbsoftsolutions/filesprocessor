using AuthenticationSvc.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationSvc
{
    class JwtTokenProcessor: ITokenProcessor
    {
        private readonly IConfiguration _configuration;
        public JwtTokenProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateToken(IApplicationUsers user, IList<string> userRoles)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Authentication,"JWT")
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
           
        }

        public string GetToken(IApplicationUsers user, IList<string> userRoles)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(GenerateToken(user, userRoles));
            return jwt;
        }

        public string GetToken(Guid id, IList<string> userRoles)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(GenerateToken(new ApplicationUser() {UserId =id, UserName="internal" }, userRoles));
            return jwt;
        }
    }

    public class ApplicationUser : IApplicationUsers
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
    }
}
