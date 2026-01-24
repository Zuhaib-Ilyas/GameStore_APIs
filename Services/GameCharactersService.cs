using GameStore_API.DTOs;
using GameStore_API.Models;

namespace GameStore_API.Services
{
    public class GameCharactersService : IGameCharactersService
    {

        // In-memory store (for example)
        private static List<Items> Game = new()
        {
            new Items { Id = 1, Name = "Cyberpunk 2077", Genre = "RPG", Price = 59.99m, ReleaseDate = new DateTime(2020, 12, 10) },
            new Items { Id = 2, Name = "Elden Ring", Genre = "Action RPG", Price = 59.99m, ReleaseDate = new DateTime(2022, 2, 25) }
        };
        public async Task<IEnumerable<ItemRequestResponse>> GetItemsAsync()
        {
            var dtos = Game.Select(
               g => new ItemRequestResponse(
                   g.Id,
                   g.Name,
                   g.ReleaseDate,
                   g.Genre
                   )
                );
            return await Task.FromResult(dtos);
        }

        public async Task<ItemRequestResponse?> GetItemsAsyncById(int id)
        {
            var result = Game.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return null;    
            }
            return await Task.FromResult(new ItemRequestResponse(
                result.Id,
                result.Genre,
                result.ReleaseDate,
                result.Name
                ));
        }
    }
}
