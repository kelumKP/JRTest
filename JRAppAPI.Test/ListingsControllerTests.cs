using JRAppAPI.Controllers;
using JRAppAPI.Models;
using JRAppAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRAppAPI.Test
{
    public class ListingsControllerTests
    {
        [Fact]
        public async Task GetListings_ReturnsOkResult()
        {
            // Arrange
            var passengers = 2;
            var mockService = new Mock<IListingService>();
            var controller = new ListingsController(mockService.Object);

            var listings = new List<Listing>
            {
                new Listing { Name = "Listing1", PricePerPassenger = 10, VehicleType = new VehicleType { MaxPassengers = 4 } },
                new Listing { Name = "Listing2", PricePerPassenger = 15, VehicleType = new VehicleType { MaxPassengers = 2 } },
            };

            mockService.Setup(service => service.GetFilteredAndSortedListings(passengers)).ReturnsAsync(listings);

            // Act
            var result = await controller.GetListings(passengers) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var model = result.Value as List<Listing>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count); // Ensure the correct number of listings is returned
        }


    }
}
