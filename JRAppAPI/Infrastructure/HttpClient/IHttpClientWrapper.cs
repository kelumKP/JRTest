/*
 * File Author: Kelum
 * License: MIT
 */

using System.Net.Http;
using System.Threading.Tasks;

namespace JRAppAPI.Infrastructure.HttpClient
{
    /// <summary>
    /// Represents an HTTP client wrapper interface for making HTTP requests.
    /// </summary>
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// Sends an HTTP GET request to the specified URI.
        /// </summary>
        /// <param name="requestUri">The URI of the resource to request.</param>
        /// <returns>A task representing the asynchronous operation and containing the HTTP response message.</returns>
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }
}

