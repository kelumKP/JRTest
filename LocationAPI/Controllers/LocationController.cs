using LocationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("{ipAddress}")]
        public async Task<IActionResult> GetLocation(string ipAddress)
        {
            var location = await _locationService.GetLocationByIpAddressAsync(ipAddress);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }
    }
}

