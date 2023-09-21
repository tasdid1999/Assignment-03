using Ecom.ClientEntity.Request.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.authRepository
{
    public interface IAuthRepository
    {
        Task<bool> LoginAsync(UserLoginRequest user);

        Task<bool> RegisterAsync(UserRegisterRequest user);

    }
}
