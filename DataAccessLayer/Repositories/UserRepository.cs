
using MinimalGameDataLibrary;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private GameDbContext _dbContext;

        public UserRepository(GameDbContext dbContext) => _dbContext = dbContext;
        public UserRepository() { }
        public void SetupDbContext(GameDbContext dbContext) => _dbContext = dbContext;

        public async Task AddUserAsync(UserData user) => await _dbContext.AddUser(user);
        public async Task<UserData?> GetUserAByUsernameAsync(string username) => await _dbContext.FindUser(username);
        public async Task<IEnumerable<UserData>> GetUsers() => await _dbContext.Users.ToListAsync();
        public async Task<IEnumerable<UserData>> GetUser(string name)
        {
            return null;
        }

        public async Task<IEnumerable<UserData>> DeleteUser(string name)
        {
            return null;
        }
    }
}
