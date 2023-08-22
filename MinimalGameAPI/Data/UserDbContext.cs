using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MinimalGameDataLibrary;

namespace MinimalGameAPI.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<UserData> Users { get; private set; }

        public async Task AddUser(UserData user)
        {
            Users.Add(user);
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserdataConfiguration());
        }

        public async Task<UserData?> FindUser(string username) => await Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public class UserdataConfiguration : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.HasKey(u => u.Id);
        }
    }
}
