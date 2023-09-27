using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using Microsoft.AspNetCore.Authorization;
using DataTransferObjects.DataTransferObjects.UserDTOs;

namespace DataAccessLayer.Controllers
{
    [Route("/api/users")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                return Ok(users);   
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
