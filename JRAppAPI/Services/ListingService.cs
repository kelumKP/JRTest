using System.Collections.Generic;
using System.Threading.Tasks;
using JRAppAPI.Models;
using JRAppAPI.Repositories; // Add this using statement

namespace JRAppAPI.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingsRepository _listingsRepository;

        public ListingService(IListingsRepository listingsRepository)
        {
            _listingsRepository = listingsRepository;
        }

        public async Task<List<Listing>> GetFilteredAndSortedListings(int passengers)
        {
            // Delegate the work to the listings repository
            return await _listingsRepository.GetFilteredAndSortedListings(passengers);
        }
    }
}

