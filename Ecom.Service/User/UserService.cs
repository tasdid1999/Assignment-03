using Ecom.Entity.Helper;
using Ecom.Infrastructure.Repository.UserRepository;
using Ecom.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

   
        public async Task<UserForToken> GetUserForTokenAsync(string email)
        {
            return await _unitOfWork.UserRepository.GetUserForTokenAsync(email);
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _unitOfWork.UserRepository.IsEmailExistAsync(email);
        }

        public async Task<bool> IsUserExistAsync(string email, string password)
        {
            return await _unitOfWork.UserRepository.IsUserExistAsync(email, password);
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            return await _unitOfWork.UserRepository.IsUserExistAsync(email);
        }
    }
}
