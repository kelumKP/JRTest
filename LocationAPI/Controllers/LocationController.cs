using LocationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LocationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationService locationService, ILogger<LocationController> logger)
        {
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get location information by IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address to look up.</param>
        /// <returns>Returns location information if found, otherwise NotFound.</returns>
        [HttpGet("{ipAddress}")]
        public async Task<IActionResult> GetLocation(string ipAddress)
        {
            try
            {
                var location = await _locationService.GetLocationByIpAddressAsync(ipAddress);

                if (location == null)
                {
                    _logger.LogWarning($"Location not found for IP address: {ipAddress}");
                    return NotFound();
                }

                return Ok(location);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while processing the request: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
