using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using MinimalGameDataLibrary.DataTransferObjects;
using DataTransferObjects.DataTransferObjects;
using MinimalGameDataLibrary.OperationResults;

namespace DataAccessLayer.Controllers
{
    [Route("api/players")]
    [ApiController]
    //[Authorize(Roles = "Admin, User")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService) => _playerService = playerService;

        #region Create
        [HttpPost("PostPlayer")]
        public async Task<IActionResult> PostPlayer([FromBody] PlayerInputDto playerInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var creationResponse = await _playerService.CreatePlayer(playerInput);

                if (creationResponse.Success)
                    return Ok(creationResponse);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, creationResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }
        #endregion


        #region Read
        [HttpGet("GetPlayers")]
        public async Task<ActionResult<IEnumerable<PlayerOutputDto>>> GetPlayers()
        {
            try
            {
                var players = await _playerService.GetPlayers();
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("GetPlayer/{id}")]
        public async Task<ActionResult<PlayerOutputDto>> GetPlayer(int id)
        {
            try
            {
                var player = await _playerService.GetPlayer(id);
                return player == null ? NotFound("No Player by this id was found.") : Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        // FIXME: Get user from db and then navigate to player.
        [HttpGet("GetPlayerFromUser/{id}")]
        public async Task<ActionResult<PlayerOutputDto>> GetPlayerFromUser(int id)
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

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [HttpDelete("Delete/id")]
        public async Task<ActionResult<PlayerOperationResult>> DeleteAPlayer(int id)
        {
            try
            {
                var result = await _playerService.DeletePlayer(id);

                if (!result.Success)
                    return NotFound(result);

                var response = new PlayerOperationResult
                {
                    Success = true,
                    Message = "Player deleted successfully.",
                    PlayerId = id
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new PlayerOperationResult
                {
                    Success = false,
                    Message = "An error occurred while deleting the player with id: " + id +
                        ", " + ex.Message,
                    ErrorCode = "DeleteError",
                };

                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }
        }

        #endregion
    }
}
