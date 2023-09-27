using JRAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JRAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingsController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListings(int passengers)
        {
            var filteredAndSorted = await _listingService.GetFilteredAndSortedListings(passengers);
            return Ok(filteredAndSorted);
        }
    }

}
