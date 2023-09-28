using LocationAPI.Infrastructure.HttpClient;
using LocationAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace LocationAPI.Repositories
{
    /// <summary>
    /// Provides a repository for retrieving location information by IP address.
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly ILogger<LocationRepository> _logger;

        public LocationRepository(
            IOptions<AppSettings> appSettings,
            IHttpClientWrapper httpClientWrapper,
            ILogger<LocationRepository> logger)
        {
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _httpClientWrapper = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets location information by IP address asynchronously.
        /// </summary>
        /// <param name="ipAddress">The IP address to look up.</param>
        /// <returns>A Task representing the asynchronous operation and containing the location information.</returns>
        public async Task<LocationModel> GetLocationByIpAddressAsync(string ipAddress)
        {
            try
            {
                // Use the API key from AppSettings.
                var apiKey = _appSettings.IpStackApiKey;
                var apiUrl = _appSettings.IpStackApiUrl;

                // Construct the API endpoint URL.
                var apiPath = $"{apiUrl}{ipAddress}?access_key={apiKey}";

                // Use the injected HttpClientWrapper to make the HTTP request.
                var response = await _httpClientWrapper.GetAsync(apiPath);

                if (response.IsSuccessStatusCode)
                {
                    // Output the JSON response string for debugging
                    var json = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a LocationModel object.
                    var locationData = JsonSerializer.Deserialize<LocationModel>(json);

                    return locationData;
                }
                else
                {
                    // Handle non-successful HTTP status codes here.
                    _logger.LogError($"HTTP request failed with status code {response.StatusCode}");
                    // You can log the error, throw an exception, or return a default location model.
                    // For example, return null for simplicity.
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here.
                _logger.LogError($"An error occurred: {ex.Message}");
                // You can log the exception and return a default location model or rethrow the exception.
                // For example, return null for simplicity.
                return null;
            }
        }
    }
}

