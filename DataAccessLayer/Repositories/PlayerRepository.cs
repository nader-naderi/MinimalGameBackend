using Microsoft.EntityFrameworkCore;
using MinimalGameDataLibrary;
using DataAccessLayer.Data;

namespace DataAccessLayer.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PlayerDbContext _dbContext;
        public PlayerRepository(PlayerDbContext dbContext) => _dbContext = dbContext;
        public async Task AddAsync(PlayerData entity) => await _dbContext.AddPlayer(entity);
        public async Task DeleteAsync(PlayerData entity) => await _dbContext.RemovePlayer(entity);
        public async Task UpdateAsync(PlayerData entity) => await _dbContext.UpdatePlayer(entity);
        public async Task<IEnumerable<PlayerData>> GetAllAsync() => await _dbContext.PlayerData.ToListAsync();
        public async Task<PlayerData?> GetById(int id) => await _dbContext.FindPlayer(id);

        public async Task<IEnumerable<PlayerData>> GetTopScorePlayersAsync(int count)
            => await _dbContext.PlayerData.OrderByDescending(p => p.Score).Take(count).ToListAsync();
        
        public async Task<IEnumerable<PlayerData>> GetTopLevelPlayersAsync(int count)
            => await _dbContext.PlayerData.OrderByDescending(p => p.Level).Take(count).ToListAsync();

        public async Task DeleteAllAsync() => await _dbContext.DeleteAllPlayers();
    }
}
