using GameDataLibrary;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TestGameAPI.Data;

namespace TestGameAPI.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : Controller
    {
        GameDbContext _dbContext;

        public PlayersController(GameDbContext gameDbContext)
        {
            _dbContext = gameDbContext;
        }

        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            return Ok(_dbContext.Players);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id)
        {
            var player = _dbContext.Players.Find(id);
            if (player == null)
                return NotFound("No record found from this id");
            else
                return Ok(player);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlayerData data)
        {
            // Check if the UserData and CharacterData objects are already existing
            // in the database (assuming they're previously created and have IDs)

            if (_dbContext.Players.Find(data.Id) != null)
            {
                var existingUserData = _dbContext.Players.Find(data.Id).UserData;
                var existingCharacterData = _dbContext.Players.Find(data.Id).CharacterData;

                // If they exist, associate them with the new PlayerData
                if (existingUserData != null && existingCharacterData != null)
                {
                    data.UserData = existingUserData;
                    data.CharacterData = existingCharacterData;
                }
            }

            // Add the new PlayerData to the Players DbSet and save changes
            _dbContext.Players.Add(data);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlayerData newData)
        {
            var oldData = _dbContext.Players.Find(id);

            if (oldData == null)
            {
                return NotFound("No record found from this id");
            }
            else
            {
                oldData = newData;
                _dbContext.SaveChanges();
                return Ok("Record Updated Successfully.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var player = _dbContext.Players.Find(id);
            if (player == null)
            {
                return NotFound("No record found from this id");
            }
            else
            {
                _dbContext.Players.Remove(player);
                _dbContext.SaveChanges();
                return Ok("Deleted Successfully.");
            }
        }
    }
}
