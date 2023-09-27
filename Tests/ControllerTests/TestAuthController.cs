using DataAccessLayer.Controllers;
using DataTransferObjects.DataTransferObjects.UserDTOs;

using Microsoft.AspNetCore.Mvc;

using MinimalGameDataLibrary.OperationResults;

using Moq;

using ServiceLayer.Services;

namespace MinimalGameAPI.UnitTests.ControllerTests
{
    [TestFixture]
    internal class TestAuthController
    {
        private Mock<IAuthService> _serviceMock;
        private AuthController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IAuthService>();
            _controller = new AuthController(_serviceMock.Object);
        }

        [Test]
        public async Task Register_OnSuccess_ReturnsOK()
        {
            var userId = 1;

            var userDto = new UserDto
            {
                UserName = "TestPlayer",
                Password = "TestPassword",
                UserRole = "Admin"
            };

            _serviceMock.Setup(service => service.RegisterUserAsync(userDto))
                .ReturnsAsync(new RegisterationResponse { Success = true, Id = userId, Message = "Created Successfully." });

            var result = await _controller.Register(userDto);

            Assert.That(result, Is.InstanceOf<IActionResult>());

            var okResult = result as OkObjectResult;

            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<RegisterationResponse>());
            var response = okResult.Value as RegisterationResponse;
            Assert.That(response.Id, Is.EqualTo(userId));
        }
    }
}
