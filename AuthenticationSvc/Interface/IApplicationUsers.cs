using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationSvc.Interface
{
    public interface IApplicationUsers
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Guid UserId { get; set; }
    }
}
