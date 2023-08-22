using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MinimalGameAPI.Services;

using MinimalGameDataLibrary;
using MinimalGameDataLibrary.DataTransferObjects;

namespace MinimalGameAPI.Controllers
{
    [Route("api/players")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService) => _playerService = playerService;

        #region Create
        [HttpPost("PostPlayer")]
        public async Task<IActionResult> PostPlayer([FromBody] PlayerInputDto playerInput)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var playerOutput = await _playerService.CreatePlayer(playerInput);

            if (playerOutput == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the player.");

            return StatusCode(201);
        }
        #endregion

        #region Read
        [HttpGet("GetPlayers")]
        public async Task<ActionResult<IEnumerable<PlayerOutputDto>>> GetPlayers()
        {
            var players = await _playerService.GetPlayers();
            return Ok(players);
        }

        [HttpGet("GetPlayer/{id}")]
        public async Task<ActionResult<PlayerOutputDto>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayer(id);
            return player == null ? NotFound("No Player by this id was found.") : Ok(player);
        }

        [HttpGet("GetTopScore")]
        public async Task<ActionResult<IEnumerable<PlayerOutputDto>>> GetTopScore()
            => Ok(await _playerService.GetTopScorePlayersAsync(10));

        [HttpGet("GetTopLevel")]
        public async Task<ActionResult<IEnumerable<PlayerOutputDto>>> GetTopLevel()
            => Ok(await _playerService.GetTopLevelPlayersAsync(10));
        #endregion

        #region Update
        [HttpPut("UpdatePlayer/{id}")]
        public async Task<IActionResult> PutPlayer(int id, [FromBody] PlayerInputDto newData)
        {
            try
            {
                var updatedPlayer = await _playerService.UpdatePlayer(id, newData);
                return Ok(updatedPlayer);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the player.");
            }
        }
        #endregion

        #region Delete
        // TODO: Remove it on publish.
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePlayers()
        {
            var result = await _playerService.DeleteAllPlayers();

            if (!result)
                return NotFound("No players found to delete.");

            return Ok("All players deleted.");
        }
        [HttpDelete("Delete/id")]
        public async Task<ActionResult<PlayerOutputDto>> DeleteAPlayer(int id)
        {
            var result = await _playerService.DeletePlayer(id);

            if (!result)
                return NotFound("No player found to delete.");

            return Ok("the player with ID: " + id + " has been deleted.");
        }
        #endregion
    }
}
