
using DataAccessLayer.Data;

using MinimalGameDataLibrary;
using MinimalGameDataLibrary.OperationResults;

namespace DataAccessLayer.Repositories
{
    public interface IUserRepository
    {
        Task<UserData?> GetUserAByUsernameAsync(string username);
        Task AddUserAsync(UserData user);
        public void SetupDbContext(GameDbContext dbContext);
        Task<IEnumerable<UserData>> GetUsers();
    }
}
