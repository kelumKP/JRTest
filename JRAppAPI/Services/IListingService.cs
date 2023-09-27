using JRAppAPI.Models;

namespace JRAppAPI.Services
{
    public interface IListingService
    {
        Task<List<Listing>> GetFilteredAndSortedListings(int passengers);
    }
}
