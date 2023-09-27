using LocationAPI.Infrastructure.HttpClient;
using LocationAPI.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LocationAPI.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public LocationRepository(IOptions<AppSettings> appSettings, IHttpClientWrapper httpClientWrapper) // Update the parameter type
        {
            _appSettings = appSettings.Value;
            _httpClientWrapper = httpClientWrapper; // Assign the injected HttpClientWrapper
        }

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
                    // You can log the error, throw an exception, or return a default location model.
                    // For example, return null for simplicity.
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here.
                // You can log the exception and return a default location model or rethrow the exception.
                // For example, return null for simplicity.
                return null;
            }
        }
    }
}
