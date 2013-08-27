using System;
using System.Collections.Generic;

namespace Seekit {
    public static class QueryExtensions {

        public static bool WithinDistanceFrom(this GeoLocation gl, GeoLocation geoLocation, Int32 km) {
            throw new InvalidOperationException("WithinDistanceFrom is only a marker method and should not be used outside the SearchClient 'Where' method");
        }

        public static bool WhereAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            throw new InvalidOperationException("Where with a bool return value is only a marker method and should not be used outside the SearchClient 'Where' method");
        }
    }
}
