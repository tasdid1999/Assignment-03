using Ecom.ClientEntity.Request.Auth;
using Ecom.Entity.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.authService
{
    public interface IAuthService
    {
        //Task<bool> ForgotPassword(string email);
        Task<bool> RegisterAsync(UserRegisterRequest user);

        Task<bool> LoginAsync(UserLoginRequest user);
        string GetJwtToken(UserForToken user);

    }
}
