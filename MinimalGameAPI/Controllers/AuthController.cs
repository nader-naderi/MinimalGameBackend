using Microsoft.AspNetCore.Mvc;
using MinimalGameAPI.Services;
using MinimalGameDataLibrary.Registeration;

namespace MinimalGameAPI.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var registrationResponse = await _authService.RegisterUserAsync(request);

            if (registrationResponse.Success)
                return Ok(registrationResponse); // 200 OK
            else
                return StatusCode(StatusCodes.Status500InternalServerError, registrationResponse); // 500 Internal Server Error
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            var loginResponse = await _authService.LoginUserAsync(request);

            if (loginResponse.Success)
                return Ok(loginResponse); // 200 OK
            else if (loginResponse.ErrorCode == "User not found.")
                return NotFound(loginResponse); // 404 Not Found
            else
                return BadRequest(loginResponse); // 400 Bad Request
        }
    }
}
