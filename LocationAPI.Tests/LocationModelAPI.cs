using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LocationAPI.Tests
{
    /// <summary>
    /// Represents a model for location information (for testing purposes).
    /// </summary>
    public class LocationModelAPI
    {
        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        [JsonPropertyName("country_name")]
        public string country_name { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public float latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public float longitude { get; set; }
    }
}

