using LocationAPI.Controllers;
using LocationAPI.Models;
using LocationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Threading.Tasks;

namespace LocationAPI.Tests
{
    [TestClass]
    public class LocationControllerTests
    {
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
                // Add other properties as needed
            };

            var locationServiceMock = new Mock<ILocationService>();
            locationServiceMock.Setup(service => service.GetLocationByIpAddressAsync(ipAddress))
                .ReturnsAsync(expectedLocation);

            var controller = new LocationController(locationServiceMock.Object);

            // Act
            var result = await controller.GetLocation(ipAddress);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var location = okResult.Value as LocationModel;
            Assert.IsNotNull(location);
            Assert.AreEqual(expectedLocation.City, location.City);
            Assert.AreEqual(expectedLocation.Country, location.Country);
            // Assert other properties as needed
        }

        [TestMethod]
        public async Task GetLocation_InvalidIpAddress_ReturnsNotFound()
        {
            // Arrange
            var ipAddress = "invalid-ip-address";
            var locationServiceMock = new Mock<ILocationService>();
            locationServiceMock.Setup(service => service.GetLocationByIpAddressAsync(ipAddress))
                .ReturnsAsync((LocationModel)null); // Simulate not found

            var controller = new LocationController(locationServiceMock.Object);

            // Act
            var result = await controller.GetLocation(ipAddress);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetLocation_ServiceException_ReturnsInternalServerError()
        {
            // Arrange
            var ipAddress = "134.201.250.155";
            var locationServiceMock = new Mock<ILocationService>();
            locationServiceMock.Setup(service => service.GetLocationByIpAddressAsync(ipAddress))
                .ThrowsAsync(new Exception("Simulated service exception"));

            var controller = new LocationController(locationServiceMock.Object);

            // Act
            var result = await controller.GetLocation(ipAddress);

            // Assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }

    }
}