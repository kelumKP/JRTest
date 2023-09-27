using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LocationAPI.Tests
{
    public class LocationModelAPI
    {
        public string city { get; set; }
        public string country_name { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
    }
}
