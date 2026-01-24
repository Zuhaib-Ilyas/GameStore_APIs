using GameStore_API.DTOs;
using GameStore_API.Models;
using GameStore_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

namespace GameStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStoreController(IGameCharactersService service) : ControllerBase
    {
        

        // GET api/games
        [HttpGet]
        public async Task< ActionResult<IEnumerable<ItemRequestResponse>>> GetGames()
        {
            Log.Information("Getting all games at {Time}", DateTime.Now);
            return Ok(await service.GetItemsAsync());  // Returns 200 OK with games list
        }

        [HttpGet("{id}")]
        public async Task< ActionResult<ItemRequestResponse>> GetItemsAsyncById(int id)
        {
            var item = await service.GetItemsAsyncById(id);
            if (item is null)
            {
                return NotFound("Id not found");
            }
            return Ok(item);  // Returns 200 OK with games list
        }
    }
}
