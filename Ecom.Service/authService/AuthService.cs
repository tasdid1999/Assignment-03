using Ecom.ClientEntity.Request.Auth;
using Ecom.Entity.Helper;
using Ecom.Infrastructure.Repository.authRepository;
using Ecom.Infrastructure.Repository.UserRepository;
using Ecom.Infrastructure.UnitOfWork;
using Ecom.Service.Token;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.authService
{
    public class AuthService : IAuthService
    {
       
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUnitOfWork _unitOfWork;
        public AuthService(UserManager<IdentityUser> userManger, IUnitOfWork unitOfWork)
        {
            _userManager = userManger;
            
            _unitOfWork = unitOfWork;
        }


        public string GetJwtToken(UserForToken user)
        {
            var tokenFactory = new JwtTokenFactory();
            return tokenFactory.CreateJWT(user);

        }

        public Task<bool> LoginAsync(UserLoginRequest user)
        {
            return _unitOfWork.AuthRepository.LoginAsync(user);
        }

        public Task<bool> RegisterAsync(UserRegisterRequest user)
        {
            return _unitOfWork.AuthRepository.RegisterAsync(user);
        }
    }
}


