using LocationAPI.Models;

namespace LocationAPI.Repositories
{
    public interface ILocationRepository
    {
        Task<LocationModel> GetLocationByIpAddressAsync(string ipAddress);
    }
}
