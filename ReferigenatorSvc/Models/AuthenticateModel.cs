using AuthenticationSvc.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigenatorSvc.Models
{
    public class AuthenticationModel : IApplicationUsers
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public Guid UserId { get; set; }
    }
}
