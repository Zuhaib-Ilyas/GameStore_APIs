using GameStore_API.DTOs;
using GameStore_API.Models;

namespace GameStore_API.Services
{
    public interface IGameCharactersService
    {

        Task<IEnumerable<ItemRequestResponse>> GetItemsAsync();
        Task<ItemRequestResponse?> GetItemsAsyncById(int id);
    }
}
