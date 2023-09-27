using Xunit;
using Moq;
using JRAppAPI.Infrastructure.HttpClient;
using JRAppAPI.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using JRAppAPI.Repositories;
using JRAppAPI.Services;

namespace JRAppAPI.Tests
{
    public class ListingsRepositoryTests
    {
        [Fact]
        public async Task GetFilteredAndSortedListings_ValidResponse_ReturnsFilteredAndSortedResults()
        {
            // Arrange
            int maxPassengersCount = 5;

            // Mock the IOptions<AppSettings> using Moq
            var mockAppSettings = new Mock<IOptions<AppSettings>>();
            var appSettings = new AppSettings
            {
                ApiUrl = "https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest"
            };
            mockAppSettings.Setup(x => x.Value).Returns(appSettings);

            // Mock the IHttpClientWrapper using Moq
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            var responseContent = @"
            {
                ""from"": ""Sydney Airport (SYD), T1 International Terminal"",
                ""to"": ""46 Church Street, Parramatta NSW, Australia"",
    ""listings"": [
    {
      ""name"": ""Listing 1"",
      ""pricePerPassenger"": 61.77,
      ""vehicleType"": {
        ""name"": ""Hatchback"",
        ""maxPassengers"": 2
      }
    },
    {
      ""name"": ""Listing 2"",
      ""pricePerPassenger"": 87.88,
      ""vehicleType"": {
        ""name"": ""Sedan"",
        ""maxPassengers"": 3
      }
    },
    {
      ""name"": ""Listing 3"",
      ""pricePerPassenger"": 35.28,
      ""vehicleType"": {
        ""name"": ""Hatchback"",
        ""maxPassengers"": 2
      }
    },
    {
      ""name"": ""Listing 4"",
      ""pricePerPassenger"": 60.08,
      ""vehicleType"": {
        ""name"": ""Sedan"",
        ""maxPassengers"": 3
      }
    },
    {
      ""name"": ""Listing 5"",
      ""pricePerPassenger"": 95.72,
      ""vehicleType"": {
        ""name"": ""SUV"",
        ""maxPassengers"": 5
      }
    },
    {
      ""name"": ""Listing 6"",
      ""pricePerPassenger"": 94.42,
      ""vehicleType"": {
        ""name"": ""SUV"",
        ""maxPassengers"": 5
      }
    },
    {
      ""name"": ""Listing 7"",
      ""pricePerPassenger"": 31.24,
      ""vehicleType"": {
        ""name"": ""Hatchback"",
        ""maxPassengers"": 2
      }
    },
    {
      ""name"": ""Listing 8"",
      ""pricePerPassenger"": 78.04,
      ""vehicleType"": {
        ""name"": ""Sedan"",
        ""maxPassengers"": 3
      }
    },
    {
      ""name"": ""Listing 9"",
      ""pricePerPassenger"": 93.24,
      ""vehicleType"": {
        ""name"": ""Sedan"",
        ""maxPassengers"": 3
      }
    },
    {
      ""name"": ""Listing 10"",
      ""pricePerPassenger"": 29.14,
      ""vehicleType"": {
        ""name"": ""Sedan"",
        ""maxPassengers"": 3
      }
    },
    {
      ""name"": ""Listing 11"",
      ""pricePerPassenger"": 99.4,
      ""vehicleType"": {
        ""name"": ""SUV"",
        ""maxPassengers"": 5
      }
    },
    {
      ""name"": ""Listing 12"",
      ""pricePerPassenger"": 16.24,
      ""vehicleType"": {
        ""name"": ""Hatchback"",
        ""maxPassengers"": 2
      }
    },
    {
      ""name"": ""Listing 13"",
      ""pricePerPassenger"": 23.42,
      ""vehicleType"": {
        ""name"": ""SUV"",
        ""maxPassengers"": 5
      }
    },
    {
      ""name"": ""Listing 14"",
      ""pricePerPassenger"": 65.29,
      ""vehicleType"": {
        ""name"": ""Hatchback"",
        ""maxPassengers"": 2
      }
    },
    {
      ""name"": ""Listing 15"",
      ""pricePerPassenger"": 3.5,
      ""vehicleType"": {
        ""name"": ""Sedan"",
        ""maxPassengers"": 3
      }
    },
    {
      ""name"": ""Listing 16"",
      ""pricePerPassenger"": 97.89,
      ""vehicleType"": {
        ""name"": ""Sedan"",
        ""maxPassengers"": 3
      }
    },
    {
      ""name"": ""Listing 17"",
      ""pricePerPassenger"": 32.56,
      ""vehicleType"": {
        ""name"": ""Hatchback"",
        ""maxPassengers"": 2
      }
    }
  ]
            }";
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            // Create an instance of ListingsRepository with the mocked dependencies
            var listingsRepository = new ListingsRepository(mockAppSettings.Object, mockHttpClientWrapper.Object);

            // Act
            var filteredAndSorted = await listingsRepository.GetFilteredAndSortedListings(maxPassengersCount);

            // Assert
            Assert.NotNull(filteredAndSorted);
            Assert.Equal(4, filteredAndSorted.Count);
            Assert.IsType<List<Listing>>(filteredAndSorted);
            // Add more assertions based on your specific requirements
        }

        [Fact]
        public async Task GetFilteredAndSortedListings_InvalidResponse_ReturnsEmptyList()
        {
            // Arrange
            int maxPassengersCount = 6;

            // Mock the IOptions<AppSettings> using Moq
            var mockAppSettings = new Mock<IOptions<AppSettings>>();
            var appSettings = new AppSettings
            {
                ApiUrl = "https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest"
            };
            mockAppSettings.Setup(x => x.Value).Returns(appSettings);

            // Mock the IHttpClientWrapper using Moq to simulate an invalid response
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
            mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            // Create an instance of ListingsRepository with the mocked dependencies
            var listingsRepository = new ListingsRepository(mockAppSettings.Object, mockHttpClientWrapper.Object);

            // Act
            var filteredAndSorted = await listingsRepository.GetFilteredAndSortedListings(maxPassengersCount);

            // Assert
            Assert.NotNull(filteredAndSorted);
            Assert.IsType<List<Listing>>(filteredAndSorted);
            Assert.Empty(filteredAndSorted); // Ensure that the result is an empty list
        }
    }
}

