using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceLayer.Services;
using DataAccessLayer.Controllers;
using MinimalGameDataLibrary.OperationResults;
using DataTransferObjects.DataTransferObjects.PlayerDTOs;

namespace MinimalGameAPI.UnitTests.ControllerTests
{
    [TestFixture]
    public class TestPlayerController
    {
        private Mock<IPlayerService> _serviceMock;
        private PlayerController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IPlayerService>();
            _controller = new PlayerController(_serviceMock.Object);
        }

        [Test]
        public async Task GetPlayerById_OnSuccess_ReturnNotFound()
        {
            var playerId = 999;
            _serviceMock.Setup(service => service.GetPlayer(playerId)).ReturnsAsync((PlayerGetResponse)null);

            var result = await _controller.GetPlayer(playerId);
            var actionResult = result.Result as NotFoundObjectResult;
            Assert.That(actionResult, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task PostPlayer_OnFailure_ValidationErrors()
        {
            var playerInput = new PlayerInputDto { /* Invalid data */ };

            _controller.ModelState.AddModelError("PropertyName", "Validation error message.");

            var result = await _controller.PostPlayer(playerInput);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }


        [Test]
        public async Task UpdatePlayer_OnSuccess_ReturnsUpdatedPlayer()
        {
            var playerId = 1;
            var playerInput = new PlayerInputDto { Name = "Updated Player" };
            var updatedPlayer = new PlayerOutputDto { Id = playerId, Name = "Updated Player" };

            var getResponse = new PlayerGetResponse
            {
                Success = true,
                Message = "Player Updated Successfully",
                PlayerData = updatedPlayer
            };

            _serviceMock.Setup(service => service.UpdatePlayer(playerId, playerInput))
                .ReturnsAsync(getResponse);

            var result = await _controller.PutPlayer(playerId, playerInput);
            var okResult = result as OkObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<IActionResult>());
                Assert.That(okResult.StatusCode, Is.EqualTo(200));
                Assert.That(okResult.Value, Is.EqualTo(getResponse));
            });
        }

        [Test]
        public async Task UpdatePlayer_OnFailure_InternalServerError()
        {
            var playerId = 1;
            var playerInput = new PlayerInputDto { Name = "Updated player" };

            _serviceMock.Setup(service => service.UpdatePlayer(playerId, playerInput))
                .ThrowsAsync(new Exception("Simulated error"));

            var result = await _controller.PutPlayer(playerId, playerInput);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var internalServerErrorResult = result as ObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(internalServerErrorResult.StatusCode, Is.EqualTo(500));
                Assert.That(internalServerErrorResult.Value, Is.InstanceOf<string>());
            });
        }

        [Test]
        public async Task DeleteAllPlayers_OnSuccess_ReturnsDeleteAll()
        {
            _serviceMock.Setup(service => service.DeleteAllPlayers())
                .ReturnsAsync(new PlayerOperationResult
                {
                    Success = true,
                    Message = "All players deleted successfully.",
                });

            var result = await _controller.DeletePlayers();

            var okResult = result as OkObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(okResult.StatusCode, Is.EqualTo(200));
                Assert.That(okResult.Value, Is.InstanceOf<PlayerOperationResult>());
            });
        }

        [Test]
        public async Task DeleteAllPlayers_OnFailure_ReturnsBadRequest()
        {
            _serviceMock.Setup(service => service.DeleteAllPlayers())
                .ReturnsAsync(new PlayerOperationResult()
                {
                    Success = false,
                    Message = "Failed to delete all players.",
                });

            var result = await _controller.DeletePlayers();

            var notFoundResult = result as NotFoundObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
                Assert.That(notFoundResult.Value, Is.InstanceOf<PlayerOperationResult>());
            });
        }

        [Test]
        public async Task GetPlayer_OnSuccess_ReturnPlayerById()
        {
            var playerId = 1;
            var player = new PlayerOutputDto { Id = playerId, Name = "Test Player 1" };

            _serviceMock.Setup(service => service.GetPlayer(playerId))
                .ReturnsAsync(new PlayerGetResponse
                {
                    Success = true,
                    PlayerData = player,
                });

            var result = await _controller.GetPlayer(playerId);

            var okResult = result.Result as OkObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<ActionResult<PlayerOutputDto>>());
                Assert.That(okResult.StatusCode, Is.EqualTo(200));
                Assert.That(okResult.Value, Is.InstanceOf<PlayerGetResponse>());
            });

            var response = okResult.Value as PlayerGetResponse;
            Assert.That(response.PlayerData, Is.EqualTo(player));
        }

        [Test]
        public async Task GetPlayers_ReturnsListOfPlayers()
        {
            // Arrange
            var players = new List<PlayerOutputDto>
            {
                // Create some sample players here
                new PlayerOutputDto { Id = 1, Name = "Test Player 1" },
                new PlayerOutputDto { Id = 2, Name = "Test Player 2" },
                new PlayerOutputDto { Id = 3, Name = "Test Player 3" }
            };

            _serviceMock.Setup(service => service.GetPlayers())
                .ReturnsAsync(new PlayerListResponse
                {
                    Success = true,
                    Message = "Player retrieved successfully.",
                    Players = players,
                });

            // Act
            var actionResult = await _controller.GetPlayers();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<ActionResult<IEnumerable<PlayerOutputDto>>>());
            var okResult = actionResult.Result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<PlayerListResponse>());

            var response = okResult.Value as PlayerListResponse;
            Assert.That(response.Players, Is.InstanceOf<List<PlayerOutputDto>>());

            var returnedPlayers = response.Players;

            // Add appropriate assertions to validate the returned data
            Assert.That(returnedPlayers, Has.Count.EqualTo(players.Count));
        }

        [Test]
        public async Task PostPlayer_OnSuccess_ReturnsCreatedPlayer()
        {
            var playerInput = new PlayerInputDto { Name = "Test Player", Level = 1, Score = 399 };

            var createdPlayer = new PlayerOutputDto { Id = 1, Name = "Test Player", Level = 1, Score = 100 };

            _serviceMock.Setup(service => service.CreatePlayer(playerInput))
                .ReturnsAsync(new PlayerCreationResponse { Success = true, PlayerId = 1, Message = "Created." });

            var result = await _controller.PostPlayer(playerInput);

            Assert.That(result, Is.InstanceOf<IActionResult>());
            var okResult = result as OkObjectResult;

            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<PlayerCreationResponse>());
            var response = okResult.Value as PlayerCreationResponse;

            Assert.That(response.PlayerId, Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteAPlayer_OnFailure_ReturnsNotFound()
        {
            var playerId = 999;

            _serviceMock.Setup(service => service.DeletePlayer(playerId)).ReturnsAsync(new PlayerOperationResult
            {
                Success = false,
                Message = "Player not found",
            });

            var result = await _controller.DeleteAPlayer(playerId);

            Assert.That(result, Is.InstanceOf<ActionResult<PlayerOperationResult>>());

            var actionResult = result as ActionResult<PlayerOperationResult>;

            Assert.NotNull(actionResult);
            Assert.That(actionResult.Result, Is.InstanceOf<NotFoundObjectResult>());

            var okResult = actionResult.Result as NotFoundObjectResult;

            Assert.NotNull(okResult);
            Assert.That(okResult.Value, Is.InstanceOf<PlayerOperationResult>());

            var response = okResult.Value as PlayerOperationResult;
            Assert.That(response.Success, Is.False);
            Assert.That(response.Message, Is.EqualTo("Player not found"));
        }

        [Test]
        public async Task DeleteAPlayer_OnFailure_InternalServerError()
        {
            var playerId = 999;

            _serviceMock.Setup(service => service.DeletePlayer(playerId)).ReturnsAsync(new PlayerOperationResult
            {
                Success = false,
                // No specific message provided, indicating an internal server error.
            });

            var result = await _controller.DeleteAPlayer(playerId);

            Assert.That(result, Is.InstanceOf<ActionResult<PlayerOperationResult>>());

            var actionResult = result as ActionResult<PlayerOperationResult>;

            Assert.NotNull(actionResult);
            Assert.That(actionResult.Result, Is.InstanceOf<ObjectResult>());

            var internalServerErrorResult = actionResult.Result as ObjectResult;

            Assert.NotNull(internalServerErrorResult);
            Assert.That(internalServerErrorResult.StatusCode, Is.EqualTo(404));
            Assert.That(internalServerErrorResult.Value, Is.InstanceOf<PlayerOperationResult>());

            var response = internalServerErrorResult.Value as PlayerOperationResult;
            Assert.That(response.Success, Is.False);
        }

    }
}
