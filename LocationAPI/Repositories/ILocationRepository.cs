using LocationAPI.Models;
using System.Threading.Tasks;

namespace LocationAPI.Repositories
{
    /// <summary>
    /// Provides an interface for accessing location information by IP address.
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// Gets location information by IP address asynchronously.
        /// </summary>
        /// <param name="ipAddress">The IP address to look up.</param>
        /// <returns>A Task representing the asynchronous operation and containing the location information.</returns>
        Task<LocationModel> GetLocationByIpAddressAsync(string ipAddress);
    }
}

