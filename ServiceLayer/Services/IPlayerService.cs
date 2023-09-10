using DataAccessLayer.Repositories;
using DataTransferObjects.DataTransferObjects;
using MinimalGameDataLibrary;
using MinimalGameDataLibrary.DataTransferObjects;
using MinimalGameDataLibrary.OperationResults;

namespace ServiceLayer.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetById(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAllAsync();
    }

    public interface IPlayerService
    {
        Task<PlayerCreationResponse> CreatePlayer(PlayerInputDto playerInput);
        Task<PlayerListResponse> GetPlayers();
        Task<PlayerGetResponse> GetPlayer(int id);
        Task<PlayerListResponse> GetTopScorePlayersAsync(int count);
        Task<PlayerListResponse> GetTopLevelPlayersAsync(int count);
        Task<PlayerGetResponse> UpdatePlayer(int playerId, PlayerInputDto playerInput);
        Task<PlayerOperationResult> DeleteAllPlayers();
        Task<PlayerOperationResult> DeletePlayer(int id);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository repository;
        public PlayerService(IPlayerRepository playerRepository)
        {
            repository = playerRepository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<PlayerCreationResponse> CreatePlayer(PlayerInputDto playerInput)
        {
            try
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

                await repository.AddAsync(PlayerData);

                return new PlayerCreationResponse
                {
                    Success = true,
                    Message = "Player created successfully.",
                    PlayerId = PlayerData.Id,
                };

                //return MapPlayerDataToPlayerOutputDto(PlayerData);
            }
            catch (Exception ex)
            {
                return new PlayerCreationResponse
                {
                    Success = false,
                    Message = "An error occurred while creating the player: " + ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message ?? "No Inner exception message available.",
                    ErrorCode = "Player Creation Error"
                };
            }
        }

        public async Task<PlayerGetResponse> GetPlayer(int id)
        {
            try
            {
                var playerData = await repository.GetById(id);

                if (playerData == null)
                {
                    return new()
                    {
                        Success = false,
                        Message = "Player with this id Not Found.",
                    };
                }
                else
                {
                    return new()
                    {
                        Success = true,
                        Message = "Player Get Operation Succeeded.",
                        PlayerId = playerData.Id,
                        PlayerData = MapPlayerDataToPlayerOutputDto(playerData),
                    };
                }
            }
            catch (Exception ex)
            {
                return new PlayerGetResponse
                {
                    Success = false,
                    Message = "An error occurred while retrieving the player: " + ex.Message,
                };
            }
        }


        public async Task<PlayerListResponse> GetPlayers()
        {
            try
            {
                var players = await repository.GetAllAsync();
                var playerOutputDto = players.Select(MapPlayerDataToPlayerOutputDto);

                return new PlayerListResponse
                {
                    Success = true,
                    Message = "Players retrieved successfully.",
                    Players = playerOutputDto.ToList(),
                };
            }
            catch (Exception ex)
            {
                return new PlayerListResponse
                {
                    Success = true,
                    Message = "An error occurred while retrieving players: " + ex.Message,
                    ErrorCode = "PlayerRetrievalError",
                    Players = new List<PlayerOutputDto>(),
                };
            }
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

        public async Task<PlayerListResponse> GetTopScorePlayersAsync(int count)
        {
            try
            {
                var players = await repository.GetTopScorePlayersAsync(count);
                var playerOutputDtos = players.Select(MapPlayerDataToPlayerOutputDto).ToList();
                return new PlayerListResponse
                {
                    Success = true,
                    Message = "Top score players fetched successfully.",
                    Players = playerOutputDtos,
                };
            }
            catch (Exception ex)
            {
                return new PlayerListResponse
                {
                    Success = false,
                    Message = "An Error occurred while fetching top score players: " + ex.Message,
                    ErrorCode = "FetchError",
                };
            }
        }

        public async Task<PlayerListResponse> GetTopLevelPlayersAsync(int count)
        {
            try
            {
                var players = await repository.GetTopLevelPlayersAsync(count);

                var playerOutputDtos = players.Select(MapPlayerDataToPlayerOutputDto).ToList();

                return new PlayerListResponse
                {
                    Success = true,
                    Message = "Top level players fetched successfully.",
                    Players = playerOutputDtos,
                };
            }
            catch (Exception ex)
            {
                return new PlayerListResponse
                {
                    Success = false,
                    Message = "An Error occurred while fetching top level players: " + ex.Message,
                    ErrorCode = "FetchError",
                };
            }

        }

        public async Task<PlayerGetResponse> UpdatePlayer(int playerId, PlayerInputDto playerInput)
        {
            try
            {
                var existingPlayer = await repository.GetById(playerId) ?? throw new Exception("Player not found");

                if (existingPlayer == null)
                {
                    return new PlayerGetResponse
                    {
                        Success = false,
                        Message = "Player not found.",
                        ErrorCode = "NotFound.",
                    };
                }

                existingPlayer.Name = playerInput.Name;
                existingPlayer.Level = playerInput.Level;
                existingPlayer.Score = playerInput.Score;
                existingPlayer.PlayerPosition = playerInput.PlayerPosition;
                existingPlayer.CoinPosition = playerInput.CoinPosition;

                await repository.UpdateAsync(existingPlayer);

                return new PlayerGetResponse
                {
                    Success = true,
                    Message = "Player updated successfully.",
                    PlayerId = existingPlayer.Id,
                    PlayerData = MapPlayerDataToPlayerOutputDto(existingPlayer),
                };
            }
            catch (Exception ex)
            {
                return new PlayerGetResponse
                {
                    Success = false,
                    Message = "An error occurred while updating the player: " + ex.Message,
                    ErrorCode = "UpdateError",
                };
            }
        }

        public async Task<PlayerOperationResult> DeleteAllPlayers()
        {
            try
            {
                await repository.DeleteAllAsync();
                return new PlayerOperationResult
                {
                    Success = true,
                    Message = "All players deleted successfully.",
                };
            }
            catch (Exception ex)
            {
                return new PlayerOperationResult()
                {
                    Success = false,
                    Message = "An error occurred while deleting the player: " + ex.Message,
                    ErrorCode = "DeletingAllPlayersError."
                };
            }
        }

        public async Task<PlayerOperationResult> DeletePlayer(int id)
        {
            try
            {
                var player = await repository.GetById(id);
                if (player == null)
                    return new PlayerOperationResult
                    {
                        Success = false,
                        Message = "Player not found.",
                        ErrorCode = "NotFound.",
                    };

                await repository.DeleteAsync(player);
                return new PlayerOperationResult
                {
                    Success = true,
                    Message = "Player deleted successfully.",
                    PlayerId = id
                };
            }
            catch (Exception ex)
            {
                return new PlayerOperationResult
                {
                    Success = false,
                    Message = "An error occurred while deleting the player: " + ex.Message,
                    ErrorCode = "DeleteError.",
                };
            }
        }
    }
}
