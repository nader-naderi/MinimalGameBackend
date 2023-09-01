using Microsoft.EntityFrameworkCore;

using MinimalGameDataLibrary;

namespace DataAccessLayer.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<GameData> GameDatas { get; set; }

        public async Task AddGame(GameData gameData)
        {
            GameDatas.Add(gameData);
            await SaveChangesAsync();
        }

        public async Task UpdateGame(int id)
        {
            GameData data = await GameDatas.FindAsync(id) ?? new GameData();

            if (data == null)
                return;

            GameDatas.Update(data);
            await SaveChangesAsync();
        }
    }
}