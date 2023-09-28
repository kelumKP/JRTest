/*
 * File Author: Kelum
 * License: MIT
 */

using System.Collections.Generic;

namespace JRAppAPI.Models
{
    /// <summary>
    /// Represents the response for a search operation.
    /// </summary>
    public class SearchResponse
    {
        /// <summary>
        /// Gets or sets the origin of the search.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the destination of the search.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the list of listings matching the search criteria.
        /// </summary>
        public List<Listing> Listings { get; set; }
    }
}

