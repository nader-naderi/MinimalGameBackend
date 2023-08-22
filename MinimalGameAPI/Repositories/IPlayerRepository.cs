using Microsoft.EntityFrameworkCore;

using MinimalGameAPI.Data;

using MinimalGameDataLibrary;
using MinimalGameDataLibrary.DataTransferObjects;

using System.Collections.Generic;

namespace MinimalGameAPI.Repositories
{
    public interface IRepository<T>
    {
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAllAsync();
    }

    public interface IPlayerRepository : IRepository<PlayerData>
    {
        Task<IEnumerable<PlayerData>> GetTopScorePlayersAsync(int count);
        Task<IEnumerable<PlayerData>> GetTopLevelPlayersAsync(int count);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly GameDbContext _dbContext;
        public PlayerRepository(GameDbContext dbContext) => _dbContext = dbContext;
        public async Task AddAsync(PlayerData entity)
        {
            _dbContext.Players.Add(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(PlayerData entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(PlayerData entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<PlayerData>> GetAllAsync() => await _dbContext.Players.ToListAsync();
        public async Task<PlayerData?> GetById(int id) => await _dbContext.FindPlayer(id);
        public async Task<IEnumerable<PlayerData>> GetTopScorePlayersAsync(int count)
            => await _dbContext.Players.OrderByDescending(p => p.Score).Take(count).ToListAsync();
        public async Task<IEnumerable<PlayerData>> GetTopLevelPlayersAsync(int count)
            => await _dbContext.Players.OrderByDescending(p => p.Level).Take(count).ToListAsync();
        public async Task DeleteAllAsync()
        {
            var players = await _dbContext.Players.ToListAsync();

            if (players.Count > 0)
            {
                _dbContext.Players.RemoveRange(players);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
