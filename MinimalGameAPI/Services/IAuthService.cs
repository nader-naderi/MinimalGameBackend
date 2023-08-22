using MinimalGameAPI.Repositories;
using MinimalGameDataLibrary;
using MinimalGameDataLibrary.Registeration;

namespace MinimalGameAPI.Services
{
    public interface IAuthService
    {
        Task<RegisterationResponse> RegisterUserAsync(UserDto request);
        Task<LoginResponse> LoginUserAsync(UserDto request);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<RegisterationResponse> RegisterUserAsync(UserDto request)
        {
            try
            {
                UserData? existingUser = await _userRepository.GetUserAByUsernameAsync(request.UserName);
                if (existingUser != null)
                {
                    RegisterationResponse existingUserResponse = new()
                    {
                        Success = false,
                        Message = "Username already exists. Please choose a different username.",
                    };

                    return existingUserResponse;
                }

                string paswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                if (request.UserRole == string.Empty || request.UserRole == "string" || request.UserRole != "User" || request.UserRole != "Admin")
                    request.UserRole = "User";

                UserData newUser = new()
                {
                    Username = request.UserName,
                    PasswordHash = paswordHash,
                    Role = request.UserRole,
                };

                await _userRepository.AddUserAsync(newUser);

                RegisterationResponse succededRegisterationResponse = new()
                {
                    Success = true,
                    Message = "User registered successfully.",
                };

                return succededRegisterationResponse;
            }
            catch (Exception ex)
            {
                RegisterationResponse failedRegisterationResponse = new()
                {
                    Success = false,
                    Message = "User registeration failed.",
                    ErrorCode = ex.Message
                };

                return failedRegisterationResponse;
            }
        }

        public async Task<LoginResponse> LoginUserAsync(UserDto request)
        {
            try
            {
                // TODO: When i Integerat the DB. Get user by username from db.
                UserData? user = await _userRepository.GetUserAByUsernameAsync(request.UserName);

                if (user == null)
                {
                    LoginResponse noUserFoundResponse = new()
                    {
                        Success = false,
                        ErrorCode = "User not found.",
                    };

                    return noUserFoundResponse;
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    LoginResponse wrongPasswordResponse = new()
                    {
                        Success = false,
                        ErrorCode = "Wrong Password",
                    };

                    return wrongPasswordResponse;
                }

                if (request.UserRole == string.Empty || request.UserRole == "string" || request.UserRole != "User" || request.UserRole != "Admin")
                    request.UserRole = "User";

                string token = _tokenService.CreateToken(user, new() { request.UserRole });

                LoginResponse succeesResponse = new()
                {
                    Success = true,
                    Token = token,
                };

                return succeesResponse;
            }
            catch (Exception ex)
            {
                LoginResponse errorRespone = new()
                {
                    Success = false,
                    Token = ex.Message,
                };

                return errorRespone;
            }
        }
    }
}
