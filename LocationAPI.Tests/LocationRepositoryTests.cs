using LocationAPI.Infrastructure.HttpClient;
using LocationAPI.Models;
using LocationAPI.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LocationAPI.Tests
{
    [TestClass]
    public class LocationRepositoryTests
    {
        [TestMethod]
        public async Task GetLocationByIpAddressAsync_ValidIpAddress_ReturnsLocation()
        {
            // Arrange
            var ipAddress = "66.249.70.130";
            var expectedLocation = new LocationModelAPI
            {
                city = "Los Angeles",
                country_name = "United States",
                latitude = 34.04759979248047f,
                longitude = -118.29226684570312f
                // Add other properties as needed
            };

            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            httpClientWrapperMock.Setup(client => client.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectedLocation))
                });

            var appSettings = new AppSettings
            {
                IpStackApiKey = "355be62c4160640834927af2d74e2ab6",
                IpStackApiUrl = "http://api.ipstack.com/" // Replace with your API URL
            };

            var appSettingsOptions = Options.Create(appSettings);

            var repository = new LocationRepository(appSettingsOptions, httpClientWrapperMock.Object); // Inject IHttpClientWrapper

            // Act
            var location = await repository.GetLocationByIpAddressAsync(ipAddress);

            // Assert
            Assert.IsNotNull(location);
            Assert.AreEqual(expectedLocation.city, location.City);
            Assert.AreEqual(expectedLocation.country_name, location.Country);
            // Assert other properties as needed
        }

        [TestMethod]
        public async Task GetLocationByIpAddressAsync_InvalidIpAddress_ReturnsNull()
        {
            // Arrange
            var ipAddress = "invalid-ip-address";
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();

            var appSettings = new AppSettings
            {
                IpStackApiKey = "355be62c4160640834927af2d74e2ab6",
                IpStackApiUrl = "http://api.ipstack.com/" // Replace with your API URL
            };

            var appSettingsOptions = Options.Create(appSettings);

            var repository = new LocationRepository(appSettingsOptions, httpClientWrapperMock.Object);

            // Act
            var location = await repository.GetLocationByIpAddressAsync(ipAddress);

            // Assert
            Assert.IsNull(location);
        }


        [TestMethod]
        public async Task GetLocationByIpAddressAsync_HttpError_ReturnsNull()
        {
            // Arrange
            var ipAddress = "134.201.250.155";
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            httpClientWrapperMock.Setup(client => client.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound // Simulate an HTTP error
                });

            var appSettings = new AppSettings
            {
                IpStackApiKey = "355be62c4160640834927af2d74e2ab6",
                IpStackApiUrl = "http://api.ipstack.com/" // Replace with your API URL
            };

            var appSettingsOptions = Options.Create(appSettings);

            var repository = new LocationRepository(appSettingsOptions, httpClientWrapperMock.Object);

            // Act
            var location = await repository.GetLocationByIpAddressAsync(ipAddress);

            // Assert
            Assert.IsNull(location);
        }

    }
}
