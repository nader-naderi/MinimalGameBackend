using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Factory
{
    public class DbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            var appSettingsDirectory = FindSiblingProjectDirectory("MinimalGameAPI");

            var appSettingspath = Path.Combine(appSettingsDirectory, "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(appSettingsDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(connectionString);

            if (optionsBuilder.Options == null)
                throw new InvalidOperationException("DbContextOptions are null.");

            return Activator.CreateInstance(typeof(TContext), optionsBuilder.Options) as TContext 
                ?? throw new InvalidOperationException("Failed to create DbContext instance.");
        }

        public string FindSiblingProjectDirectory(string projectName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            while (currentDirectory != null)
            {
                var projectDirectory = Path.Combine(currentDirectory, projectName);
                if(Directory.Exists(projectDirectory))
                    return projectDirectory;

                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }

            throw new DirectoryNotFoundException("$Cannot find the '{projectName}' project directory.");
        }
    }
}
