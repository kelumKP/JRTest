/*
 * File Author: Kelum
 * License: MIT
 */

using System;
using System.Threading.Tasks;
using JRAppAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JRAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly IListingService _listingService;
        private readonly ILogger<ListingsController> _logger;

        public ListingsController(IListingService listingService, ILogger<ListingsController> logger)
        {
            _listingService = listingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetListings(int passengers)
        {
            try
            {
                // Region: Data Retrieval

                // Retrieve filtered and sorted listings based on the number of passengers
                var filteredAndSorted = await _listingService.GetFilteredAndSortedListings(passengers);

                // Region: Logging

                // Log successful retrieval
                _logger.LogInformation("Listings retrieved successfully for {Passengers} passengers", passengers);

                return Ok(filteredAndSorted);
            }
            catch (Exception ex)
            {
                // Region: Error Handling

                // Log error
                _logger.LogError(ex, "Error occurred while retrieving listings for {Passengers} passengers", passengers);

                // Return error response
                return StatusCode(500, "An error occurred while processing request.");
            }
        }
    }
}

