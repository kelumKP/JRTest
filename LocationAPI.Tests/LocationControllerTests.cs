using LocationAPI.Controllers;
using LocationAPI.Models;
using LocationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LocationAPI.Tests
{
    [TestClass]
    public class LocationControllerTests
    {
        private LocationController _controller;
        private Mock<ILocationService> _locationServiceMock;
        private Mock<ILogger<LocationController>> _loggerMock;

        [TestInitialize]
        public void Initialize()
        {
            _locationServiceMock = new Mock<ILocationService>();
            _loggerMock = new Mock<ILogger<LocationController>>();
            _controller = new LocationController(_locationServiceMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetLocation_ValidIpAddress_ReturnsOkResult()
        {
            // Arrange
            var ipAddress = "134.201.250.155";
            var expectedLocation = new LocationModel
            {
                City = "Los Angeles",
                Country = "United States",
                Latitude = 34.04759979248047f,
                Longitude = -118.29226684570312f
            };

            _locationServiceMock.Setup(service => service.GetLocationByIpAddressAsync(ipAddress))
                .ReturnsAsync(expectedLocation);

            // Act
            var result = await _controller.GetLocation(ipAddress);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            var location = (LocationModel)okResult.Value;
            Assert.AreEqual(expectedLocation.City, location.City);
            Assert.AreEqual(expectedLocation.Country, location.Country);
        }

        [TestMethod]
        public async Task GetLocation_InvalidIpAddress_ReturnsNotFound()
        {
            // Arrange
            var ipAddress = "invalid-ip-address";
            _locationServiceMock.Setup(service => service.GetLocationByIpAddressAsync(ipAddress))
                .ReturnsAsync((LocationModel)null);

            // Act
            var result = await _controller.GetLocation(ipAddress);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetLocation_ServiceException_ReturnsInternalServerError()
        {
            // Arrange
            var ipAddress = "134.201.250.155";
            _locationServiceMock.Setup(service => service.GetLocationByIpAddressAsync(ipAddress))
                .ThrowsAsync(new Exception("simulation."));

            // Act
            var result = await _controller.GetLocation(ipAddress);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var objectResult = (ObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("An error occurred while processing the request.", objectResult.Value); // Check the exception message
        }
    }
}


