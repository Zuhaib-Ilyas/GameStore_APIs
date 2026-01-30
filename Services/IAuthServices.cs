using GameStore_API.DTOs;
using GameStore_API.Models;

namespace GameStore_API.Services
{
    public interface IAuthServices
    {

        Task<User?> RegisterAsync(UserDTO request);
        Task<string?> LoginAsync(UserDTO request);
    }
}
