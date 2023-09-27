using Microsoft.EntityFrameworkCore;

using MinimalGameDataLibrary;

namespace DataAccessLayer.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        public DbSet<UserData> Users { get; set; }

        #region User Methods
        public async Task AddUser(UserData user)
        {
            Users.Add(user);
            await SaveChangesAsync();
        }

        public async Task<UserData?> FindUser(string username) => await Users.FirstOrDefaultAsync(u => u.Username == username);

        #endregion

        public DbSet<PlayerData> Players { get; set; }

        #region Player Methods
        internal async Task<PlayerData?> FindPlayer(int id) => await Players.FindAsync(id);

        public async Task RemovePlayer(PlayerData playerData)
        {
            Players.Remove(playerData);
            await SaveChangesAsync();
        }

        public async Task AddPlayer(PlayerData playerData)
        {
            Players.Add(playerData);
            await SaveChangesAsync();
        }

        public async Task UpdatePlayer(PlayerData data)
        {
            Entry(data).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAllPlayers()
        {
            var players = await Players.ToListAsync();

            if (players.Count > 0)
            {
                Players.RemoveRange(players);
                await SaveChangesAsync();
            }
        }
        #endregion
    }
}
