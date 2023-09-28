using LocationAPI.Models;
using LocationAPI.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LocationAPI.Services
{
    /// <summary>
    /// Provides a service for retrieving location information by IP address.
    /// </summary>
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<LocationService> _logger;

        public LocationService(ILocationRepository locationRepository, ILogger<LocationService> logger)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets location information by IP address asynchronously.
        /// </summary>
        /// <param name="ipAddress">The IP address to look up.</param>
        /// <returns>A Task representing the asynchronous operation and containing the location information.</returns>
        public async Task<LocationModel> GetLocationByIpAddressAsync(string ipAddress)
        {
            try
            {
                // add additional logic here if needed
                // For example, might want to validate the IP address format or handle errors.

                // Delegate the actual location retrieval to the repository asynchronously.
                return await _locationRepository.GetLocationByIpAddressAsync(ipAddress);
            }
            catch (Exception ex)
            {
                // Handle exceptions here.
                _logger.LogError($"An error occurred: {ex.Message}");
                // log the exception and return a default location model or rethrow the exception.
                // For example, return null for simplicity.
                return null;
            }
        }
    }
}
