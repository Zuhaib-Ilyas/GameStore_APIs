using GameStore_API.Data;
using GameStore_API.DTOs;
using GameStore_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameStore_API.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthServices
    {
        public async Task<string?> LoginAsync(UserDTO request)
        {

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName
            == request.UserName);


            if (user is null)
            {
                return null;
            }


            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;

            }

            string token = CreateToken(user);
            return token;
        }

        public async Task<User?> RegisterAsync(UserDTO request)
        {


            if (await context.Users.AnyAsync(u => u.UserName == request.UserName))
            {
                return null;
            }

            var user = new User();

            var hashedPassword = new PasswordHasher<User>()
               .HashPassword(user, request.Password);

            user.UserName = request.UserName;
            user.PasswordHash = hashedPassword;

            context.Add(user);
            await context.SaveChangesAsync();
            return user;
        }


        private string CreateToken(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
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
