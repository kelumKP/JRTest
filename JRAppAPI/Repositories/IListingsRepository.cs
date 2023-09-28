/*
 * File Author: Kelum
 * License: MIT
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using JRAppAPI.Models;

namespace JRAppAPI.Repositories
{
    /// <summary>
    /// Represents a repository for retrieving and managing listings.
    /// </summary>
    public interface IListingsRepository
    {
        /// <summary>
        /// Retrieves filtered and sorted listings based on the number of passengers.
        /// </summary>
        /// <param name="passengers">The number of passengers for filtering listings.</param>
        /// <returns>A task representing the asynchronous operation and containing a list of filtered and sorted listings.</returns>
        Task<List<Listing>> GetFilteredAndSortedListings(int passengers);
    }
}

