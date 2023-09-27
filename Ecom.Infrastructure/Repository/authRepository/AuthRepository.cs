using Ecom.ClientEntity.Request.Auth;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.authRepository
{
    public class AuthRepository : IAuthRepository
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository(UserManager<IdentityUser> userManager,
                             SignInManager<IdentityUser> signInManager,
                             RoleManager<IdentityRole> roleManager
                             
                             )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            
        }
        public async Task<bool> LoginAsync(UserLoginRequest user)
        {

            var dbActionResult = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);

            return dbActionResult.Succeeded ? true : false;
        }

        public async Task<bool> RegisterAsync(UserRegisterRequest user)
        {
            var identityUser = new IdentityUser()
            {
                UserName = user.Email,
                Email = user.Email,
            };


            var dbActionResult = await _userManager.CreateAsync(identityUser, user.Password);

            if (dbActionResult.Succeeded)
            {
                var newUser = await _userManager.FindByEmailAsync(identityUser.Email);

                if (!await _roleManager.RoleExistsAsync(user.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(user.Role));
                }
                await _userManager.AddToRoleAsync(newUser, user.Role);

            }

            return dbActionResult.Succeeded ? true : false;
        }
    }
}
