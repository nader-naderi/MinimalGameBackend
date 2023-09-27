using DataAccessLayer.Repositories;

using DataTransferObjects.DataTransferObjects.PlayerDTOs;

using MinimalGameDataLibrary;
using MinimalGameDataLibrary.OperationResults;

using Moq;

using ServiceLayer.Services;

namespace Tests.ServiceTests
{
    [TestFixture]
    public class TestPlayerService
    {
        private Mock<IPlayerRepository> _repositoryMock;
        private PlayerService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPlayerRepository>();
            _service = new PlayerService(_repositoryMock.Object);
        }

        [Test]
        public async Task CreatePlayer_OnSuccess_ReturnsPlayerCreationResponse()
        {
            int playerId = 0;
            // Arrange
            var playerInput = new PlayerInputDto
            {
                DateSubmitted = DateTime.Now,
                Name = "Test",
                Level = 1,
                Score = 399,
                PlayerPosition = "1, 1, 1",
                CoinPosition = "2, 2, 2",
            };

            // Create a PlayerData instance with the expected input
            var expectedPlayerData = new PlayerData
            {
                Id = playerId,
                Name = playerInput.Name,
                Level = playerInput.Level,
                Score = playerInput.Score,
                PlayerPosition = playerInput.PlayerPosition,
                CoinPosition = playerInput.CoinPosition,
                DateSubmitted = DateTime.UtcNow,
            };

            _repositoryMock.Setup(repo => repo.AddAsync(expectedPlayerData)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreatePlayer(playerInput); 

            // Assert
            Assert.That(result, Is.InstanceOf<PlayerCreationResponse>());
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.PlayerId, Is.EqualTo(expectedPlayerData.Id));
                Assert.That(result.Message, Is.EqualTo("Player created successfully."));
            });

            // Verify that the repository's AddAsync method was called with the correct arguments
            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<PlayerData>()), Times.Once);
        }

        [Test]
        public async Task CreatePlayer_OnFailure_ReturnsException()
        {

        }

        [Test] public async Task GetPlayer_OnSuccess_ReturnsPlayerListResponse()
        {

        }
    }
}
