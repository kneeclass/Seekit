using System;

namespace Seekit {
    public static class QueryExtensions {

        public static bool WithinDistanceFrom(this GeoLocation gl, GeoLocation geoLocation, Int32 km) {
            throw new InvalidOperationException("WithinDistanceFrom is only a marker method and should not be used outside the SearchClient 'Where' method");
        }

    }
}
