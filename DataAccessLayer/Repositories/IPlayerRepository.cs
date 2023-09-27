using DataAccessLayer.Data;

using MinimalGameDataLibrary;

namespace DataAccessLayer.Repositories
{
    public interface IPlayerRepository : IRepository<PlayerData>
    {
        Task<IEnumerable<PlayerData>> GetTopScorePlayersAsync(int count);
        Task<IEnumerable<PlayerData>> GetTopLevelPlayersAsync(int count);
        public void SetupDbContext(GameDbContext dbContext);
    }
}
