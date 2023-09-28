/*
 * File Author: Kelum
 * License: MIT
 */

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Add this using statement for logging

namespace JRAppAPI.Infrastructure.HttpClient
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly ILogger<HttpClientWrapper> _logger;

        public HttpClientWrapper(System.Net.Http.HttpClient httpClient, ILogger<HttpClientWrapper> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Sends an HTTP GET request to the specified URI.
        /// </summary>
        /// <param name="requestUri">The URI of the resource to request.</param>
        /// <returns>A task representing the asynchronous operation and containing the HTTP response message.</returns>
        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            try
            {
                // Region: Sending HTTP GET Request

                // Send the HTTP GET request and await the response
                var response = await _httpClient.GetAsync(requestUri);

                // Region: Logging

                // Log successful request
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("HTTP GET request to {RequestUri} succeeded with status code {StatusCode}",
                        requestUri, (int)response.StatusCode);
                }
                else
                {
                    _logger.LogError("HTTP GET request to {RequestUri} failed with status code {StatusCode}",
                        requestUri, (int)response.StatusCode);
                }

                return response;
            }
            catch (Exception ex)
            {
                // Region: Error Handling

                // Log error and re-throw
                _logger.LogError(ex, "An error occurred while sending an HTTP GET request to {RequestUri}", requestUri);
                throw;
            }
        }
    }
}

