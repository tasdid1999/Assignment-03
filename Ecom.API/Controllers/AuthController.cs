using Ecom.ClientEntity.Request.Auth;
using Ecom.Service.authService;
using Ecom.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
        {
            try
            {
                
                if (await _userService.IsEmailExistAsync(user.Email))
                {
                    return BadRequest(new { IsSuccess = false, Message = "This Email Already Registered." });
                }

               var isRegistered = await _authService.RegisterAsync(user);

                if (isRegistered)
                {
                    return Ok(new { IsSuccess = true, Message = "SuccesFully Registered"});
                }

                return BadRequest(new { IsSuccess = false, Message = "Internal Server Issue" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {

            try
            {
               

                if(!await _userService.IsUserExistAsync(user.Email, user.Password))
                {
                 return BadRequest(new { IsSuccess = false, Message = "Wrong Credential" , Token = ""});
                }

               var isLogInSucces = await _authService.LoginAsync(user);

               if(isLogInSucces)
               {
                 var token = _authService.GetJwtToken(await _userService.GetUserForTokenAsync(user.Email));

                 return Ok(new { IsSuccess = true, Message = "Login Succesful", Token = token });

               }

                return BadRequest(new { IsSuccess = false, Message = "Internal Server Issue!", Token = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
