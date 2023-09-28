using System.Net.Http;
using System.Threading.Tasks;

namespace LocationAPI.Infrastructure.HttpClient
{
    /// <summary>
    /// Represents an interface for making HTTP requests using HttpClient.
    /// </summary>
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// Sends an HTTP GET request to the specified URI and returns the response.
        /// </summary>
        /// <param name="requestUri">The URI to request.</param>
        /// <returns>An HttpResponseMessage containing the response from the server.</returns>
        Task<HttpResponseMessage> GetAsync(string requestUri);

    }
}

