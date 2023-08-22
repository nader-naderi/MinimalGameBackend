using MinimalGameAPI.Repositories;

using MinimalGameDataLibrary;
using MinimalGameDataLibrary.DataTransferObjects;

namespace MinimalGameAPI.Services
{
    public interface IPlayerService
    {
        Task<PlayerOutputDto> CreatePlayer(PlayerInputDto playerInput);
        Task<IEnumerable<PlayerOutputDto>> GetPlayers();
        Task<PlayerOutputDto?> GetPlayer(int id);
        Task<IEnumerable<PlayerOutputDto>> GetTopScorePlayersAsync(int count);
        Task<IEnumerable<PlayerOutputDto>> GetTopLevelPlayersAsync(int count);
        Task<PlayerOutputDto> UpdatePlayer(int playerId, PlayerInputDto playerInput);
        Task<bool> DeleteAllPlayers();
        Task<bool> DeletePlayer(int id);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository) => _playerRepository = playerRepository;

        public async Task<PlayerOutputDto> CreatePlayer(PlayerInputDto playerInput)
        {
            PlayerData PlayerData = new()
            {
                Name = playerInput.Name,
                Level = playerInput.Level,
                Score = playerInput.Score,
                PlayerPosition = playerInput.PlayerPosition,
                CoinPosition = playerInput.CoinPosition,
                DateSubmitted = DateTime.UtcNow
            };

            await _playerRepository.AddAsync(PlayerData);
            return MapPlayerDataToPlayerOutputDto(PlayerData);
        }

        public async Task<PlayerOutputDto?> GetPlayer(int id)
        {
            var playerData = await _playerRepository.GetById(id);

            if (playerData == null)
                return null;

            return MapPlayerDataToPlayerOutputDto(playerData);
        }

        public async Task<IEnumerable<PlayerOutputDto>> GetPlayers()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(MapPlayerDataToPlayerOutputDto);
        }

        private static PlayerOutputDto MapPlayerDataToPlayerOutputDto(PlayerData playerData)
        {
            return new PlayerOutputDto
            {
                Id = playerData.Id,
                Name = playerData.Name,
                Level = playerData.Level,
                Score = playerData.Score,
                PlayerPosition = playerData.PlayerPosition,
                CoinPosition = playerData.CoinPosition,
                DateSubmitted = playerData.DateSubmitted
            };
        }

        public async Task<IEnumerable<PlayerOutputDto>> GetTopScorePlayersAsync(int count)
        {
            var players = await _playerRepository.GetTopScorePlayersAsync(count);

            var playerOutputDtos = players.Select(MapPlayerDataToPlayerOutputDto);

            return playerOutputDtos;
        }

        public async Task<IEnumerable<PlayerOutputDto>> GetTopLevelPlayersAsync(int count)
        {
            var players = await _playerRepository.GetTopLevelPlayersAsync(count);

            var playerOutputDtos = players.Select(MapPlayerDataToPlayerOutputDto);

            return playerOutputDtos;
        }

        public async Task<PlayerOutputDto> UpdatePlayer(int playerId, PlayerInputDto playerInput)
        {
            var existingPlayer = await _playerRepository.GetById(playerId) ?? throw new Exception("Player not found");

            existingPlayer.Name = playerInput.Name;
            existingPlayer.Level = playerInput.Level;
            existingPlayer.Score = playerInput.Score;
            existingPlayer.PlayerPosition = playerInput.PlayerPosition;
            existingPlayer.CoinPosition = playerInput.CoinPosition;

            await _playerRepository.UpdateAsync(existingPlayer);
            return MapPlayerDataToPlayerOutputDto(existingPlayer);
        }

        public async Task<bool> DeleteAllPlayers()
        {
            try
            {
                await _playerRepository.DeleteAllAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletePlayer(int id)
        {
            try
            {
                var player = await _playerRepository.GetById(id);
                if (player == null)
                    return false;

                await _playerRepository.DeleteAsync(player);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
