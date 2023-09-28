using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocationAPI.Infrastructure.HttpClient
{
    /// <summary>
    /// A wrapper for HttpClient to simplify HTTP requests.
    /// </summary>
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the HttpClientWrapper class.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance to wrap.</param>
        public HttpClientWrapper(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Sends an HTTP GET request to the specified URI and returns the response.
        /// </summary>
        /// <param name="requestUri">The URI to request.</param>
        /// <returns>An HttpResponseMessage containing the response from the server.</returns>
        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            try
            {
                // Send the GET request using HttpClient.
                var response = await _httpClient.GetAsync(requestUri);

                // Check if the response was successful.
                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors (e.g., network issues).
                // You can log the error or throw a custom exception here.
                throw new Exception("HTTP request failed.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions.
                // You can log the error or throw a custom exception here.
                throw new Exception("An error occurred while making the HTTP request.", ex);
            }
        }

        // Implement other HTTP methods if needed
    }
}

