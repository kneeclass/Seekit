using System;
using System.Data;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Seekit.Connection;
using Seekit.Extensions;
using Seekit.Facets;
using Seekit.Linq;
using Seekit.Models;
using Seekit.Utils;

namespace Seekit {

    public class SearchClient<T> : SearchClientBase, ISearchClient<T>
    {
        public SearchClient(string query) : base(query)
        {
            TypeFullName = typeof (T).JsonNetFormat();
        }
        public SearchClient(string query, string lang) : base(query, lang)
        {
            TypeFullName = typeof (T).JsonNetFormat();
        }

        public ISearchClient<T> Where(Expression<Func<T, object>> expression) {
            var parser = new ExpressionParser<T>();
            var conExpressions = parser.Parse(expression.Body);
            ConvertedExpressions.AddRange(conExpressions);
            return this;
        }

        public ISearchClient<T> WithinRadiusOf(Expression<Func<T, object>> expression, GeoLocation geoLocation, double km) {
            var propertyName = ExpressionUtils.GetPropertyName(expression);
            return WithinRadiusOf(propertyName, geoLocation, km);
        }
        public ISearchClient<T> WithinRadiusOf(string propertyName, GeoLocation geoLocation, double km) {

            var tType = typeof(T);
            var property = tType.GetProperty(propertyName);
            if (property.PropertyType != typeof(GeoLocation)) {
                throw new SyntaxErrorException("The property selected as WithinRadiusOf property is not of the type GeoLocation");
            }
            GeoQuery = new GeoQuery {
                               Distance = km,
                               Latitude = geoLocation.Latitude,
                               Longitude = geoLocation.Longitude,
                               PropertyName = propertyName
                           };

            return this;
        }

        public ISearchClient<T> IncludeEmptyFacets(bool include = true) {
            ((ISearchClient<T>)this).IncEmptyFacets = include;
            return this;
        }

        public SearchResult<T> Search() {
            var requester = new SearchOperation();
            var jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var jsonData = requester.PreformSearch(JsonConvert.SerializeObject(ConvertExpression(), Formatting.None), Configuration);
            var result = JsonConvert.DeserializeObject<SearchResultContext<T>>(jsonData,jsonSerializerSettings);;
            var fcm = new FacetContextMerger<T>();
            fcm.MergeFacets(result.CrawlStamp, result.SearchResults[0].Facets, ((ISearchClient<T>)this).IncEmptyFacets, ((ISearchClient<T>)this).Lang);
            return result.SearchResults[0];
        }

        /// <summary>
        /// The number of items to retrive. The default value is 10
        /// </summary>
        /// <param name="count"></param>
        public ISearchClient<T> Take(Int32 count) {
            TakeCount = count;
            return this;
        }
        /// <summary>
        /// The number of items to skip. The default value is 0
        /// </summary>
        /// <param name="count"></param>
        public ISearchClient<T> Skip(Int32 count) {
            SkipCount = count;
            return this;
        }

        public IOrderedSearchClient<T> OrderBy(Expression<Func<T, object>> expression)
        {
            return new OrderedSearchClient<T>(this);
        }

        public IOrderedSearchClient<T> OrderBy(string propertyName)
        {
            return new OrderedSearchClient<T>(this);
        }

        public IOrderedSearchClient<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            return new OrderedSearchClient<T>(this);
        }

        public IOrderedSearchClient<T> OrderByDescending(string propertyName)
        {
            return new OrderedSearchClient<T>(this);
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(ConvertExpression());

        }

    }
}
