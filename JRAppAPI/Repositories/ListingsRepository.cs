/*
 * File Author: Kelum
 * License: MIT
 */

using JRAppAPI.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using JRAppAPI.Infrastructure.HttpClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace JRAppAPI.Repositories
{
    /// <summary>
    /// Represents a repository for retrieving and managing listings.
    /// </summary>
    public class ListingsRepository : IListingsRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly ILogger<ListingsRepository> _logger;

        public ListingsRepository(
            IOptions<AppSettings> appSettings,
            IHttpClientWrapper httpClientWrapper,
            ILogger<ListingsRepository> logger)
        {
            _appSettings = appSettings.Value;
            _httpClientWrapper = httpClientWrapper;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<Listing>> GetFilteredAndSortedListings(int passengers)
        {
            try
            {
                var apiUrl = _appSettings.ApiUrl;

                // Make an HTTP request to the search endpoint
                var response = await _httpClientWrapper.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a SearchResponse object
                var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(content);

                // Filter listings based on the number of passengers
                var filteredListings = searchResponse.Listings
                    .Where(listing => listing.VehicleType.MaxPassengers >= passengers)
                    .ToList();

                // Calculate the total price for each listing
                foreach (var listing in filteredListings)
                {
                    listing.Price = CalculateTotalPrice(listing, passengers);
                }

                // Sort the results by total price in descending order (highest total price first)
                filteredListings = filteredListings.OrderByDescending(listing => listing.Price).ToList();

                return filteredListings;
            }
            catch (HttpRequestException ex)
            {
                // Handle the HTTP request exception here (e.g., log and respond appropriately)
                // For example, you can log the error and return an empty list or throw a custom exception.
                _logger.LogError(ex, "Error while making HTTP request.");
                return new List<Listing>(); // Return an empty list or handle it based on your application's requirements.
            }
            catch (Exception ex)
            {
                // Handle other exceptions here if needed
                _logger.LogError(ex, "An error occurred.");
                throw; // Rethrow other exceptions.
            }
        }

        private decimal CalculateTotalPrice(Listing listing, int passengers)
        {
            // Calculate the total price by multiplying the PricePerPassenger by the number of passengers
            return listing.PricePerPassenger * passengers;
        }
    }
}

