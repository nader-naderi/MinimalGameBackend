using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using MinimalGameDataLibrary;

namespace DataAccessLayer.Data
{
    public class PlayerDbContext : DbContext
    {
        public PlayerDbContext(DbContextOptions<PlayerDbContext> options) : base(options)
        {
        }

        public DbSet<PlayerData> PlayerData { get; set; }

        internal async Task<PlayerData?> FindPlayer(int id) => await PlayerData.FindAsync(id);

        public async Task RemovePlayer(PlayerData playerData)
        {
            PlayerData.Remove(playerData);
            await SaveChangesAsync();
        }

        public async Task AddPlayer(PlayerData playerData)
        {
            PlayerData.Add(playerData);
            await SaveChangesAsync();
        }

        public async Task UpdatePlayer(PlayerData data)
        {
            Entry(data).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAllPlayers()
        {
            var players = await PlayerData.ToListAsync();

            if (players.Count > 0)
            {
                PlayerData.RemoveRange(players);
                await SaveChangesAsync();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerData>().HasKey(p => p.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent; // Go up two levels to reach the sibling project directory
                var appSettingsPath = Path.Combine(directory.FullName, "MinimalGameAPI", "appsettings.json"); // Construct the full path to appsettings.json

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(directory.FullName) // Use the directory containing appsettings.json
                    .AddJsonFile("appsettings.json") // Load appsettings.json
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}