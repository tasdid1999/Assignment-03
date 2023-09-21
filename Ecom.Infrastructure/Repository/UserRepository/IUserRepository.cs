using Ecom.Entity.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<bool> IsEmailExistAsync(string email);

        Task<bool> IsUserExistAsync(string email, string password);

        Task<bool> IsUserExistAsync(string email);
        Task<UserForToken> GetUserForTokenAsync(string email);
    }
}
 