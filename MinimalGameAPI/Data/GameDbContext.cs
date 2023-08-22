using Microsoft.EntityFrameworkCore;

using MinimalGameDataLibrary;

namespace MinimalGameAPI.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerData>().OwnsOne(p => p.PlayerPosition);
            modelBuilder.Entity<PlayerData>().OwnsOne(p => p.CoinPosition);
        }

        internal async Task<PlayerData?> FindPlayer(int id)
        {
            return await Players.FindAsync(id);
        }

        public DbSet<PlayerData> Players { get; set; }
    }
}
