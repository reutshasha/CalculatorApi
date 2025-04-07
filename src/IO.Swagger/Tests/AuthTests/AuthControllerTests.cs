using BL.Interfaces;
using CalculatorApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using IO.Swagger.Models;
using Moq;
using Xunit;

namespace CalculatorApi.Tests.AuthTests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthManager> _mockAuthManager;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthManager = new Mock<IAuthManager>();
            _controller = new AuthController(_mockAuthManager.Object, null);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var loginRequest = new LoginRequest { Username = "validUser", Password = "validPassword" };
            _mockAuthManager.Setup(auth => auth.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns("validToken");

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal("validToken", returnValue.Token);
        }

        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = new LoginRequest { Username = "invalidUser", Password = "invalidPassword" };
            _mockAuthManager.Setup(auth => auth.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns((string)null);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var returnValue = Assert.IsType<dynamic>(unauthorizedResult.Value);
            Assert.Equal("Invalid username or password.", returnValue.Error);
        }
    }
}
