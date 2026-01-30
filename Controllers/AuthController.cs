using GameStore_API.DTOs;
using GameStore_API.Models;
using GameStore_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {
       
        [HttpPost("register")]

        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var user = await authServices.RegisterAsync(request);

            if (user is null)
            {
                return BadRequest("Username alreaddy exists.");
            }

            return Ok(user);
        }

        [HttpPost("login")]

        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            var token = await authServices.LoginAsync(request);

            if (token is null)
            {
                return BadRequest("Invalid Username or password");
            }

                
            return Ok(token);
        }

      
       
    }
}
