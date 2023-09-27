using DataAccessLayer.Repositories;
using DataTransferObjects.DataTransferObjects.UserDTOs;
using MinimalGameDataLibrary;
using MinimalGameDataLibrary.OperationResults;

namespace ServiceLayer.Services
{
    public interface IUserService
    {
        Task<UsersListResponse> GetUsers();
    }

    public class UserService : IUserService
    {
        IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UsersListResponse> GetUsers()
        {
            try
            {
                var users = await _repository.GetUsers();
                var dtos = users.Select(MapUserDataToUserDto);

                return new UsersListResponse
                {
                    Success = true,
                    Message = "Users retrieved successfully.",
                    Users = dtos.ToList(),
                };
            }
            catch (Exception ex)
            {
                return new UsersListResponse()
                {
                    Success = false,
                    ErrorCode = "UsersRetrievalError.",
                    Message = ex.Message,
                    InternalErrorException = ex.InnerException.Message
                };
            }
        }

        private static UserRegisterationDto MapUserDataToUserDto(UserData data)
        {
            return new UserRegisterationDto
            {
                Password = data.PasswordHash,
                UserName = data.Username,
                UserRole = data.Role
            };
        }
    }
}
