using BL.DTOs;
using BL.Interfaces;
using CalculatorApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace CalculatorApi.Tests.CalculatorTests
{
    public class CalculatorControllerTests
    {
        private readonly Mock<ICalculatorService> _mockCalculatorService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly CalculatorController _controller;

        public CalculatorControllerTests()
        {
            _mockCalculatorService = new Mock<ICalculatorService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _controller = new CalculatorController(_mockCalculatorService.Object, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public void Calculate_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new CalculationsRequest { Num1 = 1, Num2 = 2 };
            _mockCalculatorService.Setup(service => service.Calculate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(3);

            // Act
            var result = _controller.Calculate(request, "Add");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal(3, returnValue.result);
        }

        [Fact]
        public void Calculate_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            CalculationsRequest request = null; // Invalid request

            // Act
            var result = _controller.Calculate(request, "Add");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid request body.", badRequestResult.Value);
        }

        [Fact]
        public void Calculate_CalculateThrowsArgumentException_ReturnsBadRequest()
        {
            // Arrange
            var request = new CalculationsRequest { Num1 = 1, Num2 = 2 };
            _mockCalculatorService.Setup(service => service.Calculate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Throws(new ArgumentException("Invalid operation"));

            // Act
            var result = _controller.Calculate(request, "InvalidOperation");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid operation", badRequestResult.Value);
        }
    }
}

