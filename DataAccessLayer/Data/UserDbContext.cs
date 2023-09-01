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
            var builder = new ConfigurationBuilder()
                .SetBasePath("C:/MyWorks/Backend/DotNetCore/DataStorageGameBackendTestAPI/TestGameBackend/MinimalGameAPI/")
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var dbConInfo = builder.Build().GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            optionsBuilder.UseSqlServer(dbConInfo);
        }
    }
}