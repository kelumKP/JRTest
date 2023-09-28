/*
 * File Author: Kelum
 * License: MIT
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using JRAppAPI.Models;
using JRAppAPI.Repositories;
using Microsoft.Extensions.Logging;

namespace JRAppAPI.Services
{
    /// <summary>
    /// Provides services for managing listings.
    /// </summary>
    public class ListingService : IListingService
    {
        private readonly IListingsRepository _listingsRepository;
        private readonly ILogger<ListingService> _logger;

        public ListingService(IListingsRepository listingsRepository, ILogger<ListingService> logger)
        {
            _listingsRepository = listingsRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<Listing>> GetFilteredAndSortedListings(int passengers)
        {
            try
            {
                // Delegate the work to the listings repository
                return await _listingsRepository.GetFilteredAndSortedListings(passengers);
            }
            catch (System.Exception ex)
            {
                // Log error and re-throw
                _logger.LogError(ex, "An error occurred while retrieving and sorting listings.");
                throw;
            }
        }
    }
}


