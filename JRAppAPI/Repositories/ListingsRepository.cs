using JRAppAPI.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using JRAppAPI.Infrastructure.HttpClient;
using System.Text.Json;

namespace JRAppAPI.Repositories
{
    public class ListingsRepository : IListingsRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public ListingsRepository(IOptions<AppSettings> appSettings, IHttpClientWrapper httpClientWrapper)
        {
            _appSettings = appSettings.Value;
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<List<Listing>> GetFilteredAndSortedListings(int passengers)
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

        private decimal CalculateTotalPrice(Listing listing, int passengers)
        {
            // Calculate the total price by multiplying the PricePerPassenger by the number of passengers
            return listing.PricePerPassenger * passengers;
        }
    }
}
