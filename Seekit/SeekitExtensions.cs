using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Seekit.Facets;

namespace Seekit {
    public static class SeekitExtensions {

        public static bool WithinDistanceFrom(this GeoLocation gl, GeoLocation geoLocation, Int32 km) {
            throw new InvalidOperationException("WithinDistanceFrom is only a marker method and should not be used outside the SearchClient 'Where' method");
        }

        public static bool WhereAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            throw new InvalidOperationException("Where with a bool return value is only a marker method and should not be used outside the SearchClient 'Where' method");
        }

        public static Facet ForProperty<T>(this FacetsList<T> facets, Expression<Func<T, object>> expression) where T : SearchModelBase
        {
            if(!(expression.Body is MemberExpression)) {
                throw new Exception("The body of the expression must be a MemberExpression");
            }
            var propertyName = ((MemberExpression) expression.Body).Member.Name;

            return facets.Where(x => x.PropetyName.Equals(propertyName)).SingleOrDefault();


        }
    }
}
