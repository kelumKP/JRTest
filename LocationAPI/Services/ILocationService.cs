using LocationAPI.Models;

namespace LocationAPI.Services
{
    public interface ILocationService
    {
        Task<LocationModel> GetLocationByIpAddressAsync(string ipAddress);
    }
}
