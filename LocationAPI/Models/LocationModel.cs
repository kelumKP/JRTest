using System.Text.Json.Serialization;

namespace LocationAPI.Models
{
    /// <summary>
    /// Represents a location model with city, country, latitude, and longitude.
    /// </summary>
    public class LocationModel
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country_name")]
        public string Country { get; set; }

        [JsonPropertyName("latitude")]
        public float Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public float Longitude { get; set; }

    }
}