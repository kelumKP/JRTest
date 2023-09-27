using System.Collections.Generic;
using System.Threading.Tasks;
using JRAppAPI.Models;

namespace JRAppAPI.Repositories
{
    public interface IListingsRepository
    {
        Task<List<Listing>> GetFilteredAndSortedListings(int passengers);
    }
}
