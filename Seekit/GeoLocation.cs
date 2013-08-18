using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seekit {
    public class GeoLocation {
        public GeoLocation(){}
        public GeoLocation(double latitude, double longitude) {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return Latitude + ", " + Longitude;
        }

    }
}
