using Microsoft.EntityFrameworkCore;

using MinimalGameAPI.Data;

using MinimalGameDataLibrary;

namespace MinimalGameAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserData?> GetUserAByUsernameAsync(string username);
        Task AddUserAsync(UserData user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext) => _dbContext = dbContext;
        public async Task AddUserAsync(UserData user) => await _dbContext.AddUser(user);
        public async Task<UserData?> GetUserAByUsernameAsync(string username) => await _dbContext.FindUser(username);
    }
}
