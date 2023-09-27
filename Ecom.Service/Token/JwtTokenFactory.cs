using Ecom.Entity.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Service.Token
{
    public class JwtTokenFactory
    {
        
        public JwtTokenFactory()
        {
            
        }

       
        public string CreateJWT(UserForToken user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(user.SecretKey);

            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name, user.UserName)
            });

            var credential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credential
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var accessToken = jwtTokenHandler.WriteToken(token);

            return accessToken;
        }
        public static string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenClaims = tokenHandler.ReadJwtToken(token);

            var userId = tokenClaims.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;

            return userId ?? "User_ID_NOT_FOUND";
        }
    }
}
