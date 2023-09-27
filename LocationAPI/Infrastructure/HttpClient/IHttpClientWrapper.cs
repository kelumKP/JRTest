namespace LocationAPI.Infrastructure.HttpClient
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
        // Add other HTTP methods as needed
    }
}
