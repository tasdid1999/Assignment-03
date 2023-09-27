using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Entity.Helper
{
    public class UserForToken
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public UserForToken(IdentityUser user, string role)
        {
            Id = user.Id;
            UserName = user.UserName;
            Role = role;

        }
        public string SecretKey { get; set; }
    }
}
