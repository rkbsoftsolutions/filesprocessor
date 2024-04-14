using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthenticationSvc.Interface
{
   public  interface ITokenProcessor
    {
        public JwtSecurityToken GenerateToken(IApplicationUsers user, IList<string> userRoles);

        public string GetToken(IApplicationUsers user, IList<string> userRoles);

        public string GetToken(Guid Id, IList<string> userRoles);
    }
}
