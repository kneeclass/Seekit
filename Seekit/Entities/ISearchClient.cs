using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Seekit.Entities {
    public interface ISearchClient<T> : ISearchable<T> {
        ISearchClient<T> Where(Expression<Func<T, object>> expression);
        ISearchClient<T> WithinRadiusOf(Expression<Func<T, object>> expression, GeoLocation geoLocation, double km);
        ISearchClient<T> WithinRadiusOf(string propertyName, GeoLocation geoLocation, double km);
        ISearchClient<T> IncludeEmptyFacets(bool include = true);

        /// <summary>
        /// The number of items to retrive. The default value is 10
        /// </summary>
        /// <param name="count"></param>
        ISearchClient<T> Take(Int32 count);

        /// <summary>
        /// The number of items to skip. The default value is 0
        /// </summary>
        /// <param name="count"></param>
        ISearchClient<T> Skip(Int32 count);

        IOrderedSearchClient<T> OrderBy(Expression<Func<T, object>> expression);
        IOrderedSearchClient<T> OrderBy(string propertyName);
        IOrderedSearchClient<T> OrderByDescending(Expression<Func<T, object>> expression);
        IOrderedSearchClient<T> OrderByDescending(string propertyName);
    }

    public interface ISearchable<T> : ISearchClient
    {
        SearchResult<T> Search();
    }

    public interface ISearchClient {
        Query Query { get; }
        string Lang { get; set; }
        bool IncEmptyFacets { get; set; }
        List<SortOrder> SortOrders { get; }
    }

}
