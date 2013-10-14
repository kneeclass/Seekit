using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Seekit.Facets;
using Seekit.Models;
using Seekit.Utils;

namespace Seekit {
    public static class SeekitExtensions {

        //public static bool WithinRadiusOf(this GeoLocation gl, GeoLocation geoLocation, double km) {
        //    throw new InvalidOperationException("WithinDistanceFrom is only a marker method and should not be used outside the SearchClient 'Where' method");
        //}

        public static bool WhereAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            throw new InvalidOperationException("Where with a bool return value is only a marker method and should not be used outside the SearchClient 'Where' method");
        }

        public static Facet ForProperty<T>(this FacetsList<T> facets, Expression<Func<T, object>> expression) where T : SearchModelBase
        {
            var propertyName = ExpressionUtils.GetPropertyName(expression);

            return facets.Where(x => x.PropetyName.Equals(propertyName)).SingleOrDefault();
        }



        public static SearchResult<T> ResultFrom<T>(this SearchResultContext<SearchModelBase> searchResultContext, SearchClient<T> searchClient) where T : SearchModelBase {
            var result = searchResultContext.SearchResults.Where(x => x.QueryId == ((ISearchClient<T>)searchClient).Query.QueryId).SingleOrDefault();
            if (result == null)
                return null;


            var newVal = new SearchResult<T>{Facets = new FacetsList<T>(), Hits = new List<SearchHit<T>>()};
            newVal.Facets.AddRange(result.Facets);
            newVal.Hits.AddRange(result.Hits.Select(x => x.ConvertType<T>()));
            newVal.Query = result.Query;
            newVal.QueryId = result.QueryId;


            return newVal;
        }
    }
}
