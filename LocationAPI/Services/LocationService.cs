using LocationAPI.Models;
using LocationAPI.Repositories;

namespace LocationAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<LocationModel> GetLocationByIpAddressAsync(string ipAddress)
        {
            // You can add additional logic here if needed
            // For example, you might want to validate the IP address format or handle errors.

            // Delegate the actual location retrieval to the repository asynchronously.
            return await _locationRepository.GetLocationByIpAddressAsync(ipAddress);
        }
    }
}
