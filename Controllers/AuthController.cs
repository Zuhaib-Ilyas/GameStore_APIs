using GameStore_API.DTOs;
using GameStore_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]

        public ActionResult<User> Register(UserDTO request)
        {
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.UserName = request.UserName;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }

        [HttpPost("login")]

        public ActionResult<string> Login(UserDTO request)
        {
            if (user.UserName != request.UserName)
            {
                return BadRequest("user not found");
            }


            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return BadRequest("Password is wrong");

            }

            string token = CreateToken(user);
            return Ok(token);
        }

        //private string CreateToken(User user)
        //{
        //    var claims = new List<Claim>
        //    {
        //            new Claim(ClaimTypes.Name, user.UserName)
        //    };

        //    var key = new SymmetricSecurityKey(
        //        Encoding.UTF8.
        //        GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
        //        );

        //    var securityAlgo = SecurityAlgorithms.HmacSha512;

        //    var creds = new SigningCredentials(
        //        key, securityAlgo);



        //    var tokenDescriptor = new JwtSecurityToken(
        //        issuer: configuration.GetValue<string>("AppSettings:issuer"),
        //        audience: configuration.GetValue<string>("AppSettings:issuer"),
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddDays(1),
        //        signingCredentials: creds

        //        );

        //    var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        //    return token;   
        //}

        private string CreateToken(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes
                (configuration.GetValue<string>("AppSettings:Token")!)
                
                );

            var algorithm = SecurityAlgorithms.HmacSha512;
            var creds = new SigningCredentials(key, algorithm);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:issuer"),
                audience: configuration.GetValue<string>("AppSettings:audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                
                );


            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }
    }
}
