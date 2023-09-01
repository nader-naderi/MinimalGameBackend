using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

using MinimalGameDataLibrary;

namespace DataAccessLayer.Data
{

    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<UserData> Users { get; set; }

        public async Task AddUser(UserData user)
        {
            Users.Add(user);
            await SaveChangesAsync();
        }

        public async Task<UserData?> FindUser(string username) => await Users.FirstOrDefaultAsync(u => u.Username == username);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().HasKey(x => x.Id);
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