using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Seekit {
    public class GeoLocation {
        public GeoLocation(){}
        public GeoLocation(double latitude, double longitude) {
            Latitude = latitude;
            Longitude = longitude;
        }
        [JsonProperty(PropertyName = "Lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "Long")]
        public double Longitude { get; set; }

    }
}
