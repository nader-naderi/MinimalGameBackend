
using MinimalGameDataLibrary;

namespace DataAccessLayer.Repositories
{
    public interface IUserRepository
    {
        Task<UserData?> GetUserAByUsernameAsync(string username);
        Task AddUserAsync(UserData user);
    }
}
