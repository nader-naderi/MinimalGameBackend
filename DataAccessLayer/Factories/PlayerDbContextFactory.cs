using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Factory
{
    public class PlayerDbContextFactory : IDesignTimeDbContextFactory<PlayerDbContext>
    {
        public PlayerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath("C:/MyWorks/Backend/DotNetCore/DataStorageGameBackendTestAPI/TestGameBackend/MinimalGameAPI/")
                    .AddJsonFile("appsettings.json")
                    .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PlayerDbContext>();

            var connectionString = configuration
                        .GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new PlayerDbContext(optionsBuilder.Options);
        }
    }
}
