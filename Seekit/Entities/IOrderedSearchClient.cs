using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Seekit.Utils;

namespace Seekit.Entities {
    public interface IOrderedSearchClient<T> : ISearchClient<T>
    {
        IOrderedSearchClient<T> ThenBy(Expression<Func<T, object>> expression);
        IOrderedSearchClient<T> ThenBy(string propertyName);
        IOrderedSearchClient<T> ThenByDescending(Expression<Func<T, object>> expression);
        IOrderedSearchClient<T> ThenByDescending(string propertyName);
    }

    public class OrderedSearchClient<T> : IOrderedSearchClient<T>
    {
        internal ISearchClient<T> Source;
        internal OrderedSearchClient(ISearchClient<T> source)
        {
            Source = source;
        }
        #region Source override
        public ISearchClient<T> Where(Expression<Func<T, object>> expression)
        {
            return Source.Where(expression);
        }

        public ISearchClient<T> WithinRadiusOf(Expression<Func<T, object>> expression, GeoLocation geoLocation, double km)
        {
            return Source.WithinRadiusOf(expression,geoLocation, km);
        }

        public ISearchClient<T> WithinRadiusOf(string propertyName, GeoLocation geoLocation, double km)
        {
            return Source.WithinRadiusOf(propertyName, geoLocation, km);
        }

        public ISearchClient<T> IncludeEmptyFacets(bool include)
        {
            return Source.IncludeEmptyFacets(include);
        }

        public SearchResult<T> Search()
        {
            return Source.Search();
        }

        public ISearchClient<T> Take(int count)
        {
            return Source.Take(count);
        }

        public ISearchClient<T> Skip(int count)
        {
            return Source.Skip(count);
        }

        public IOrderedSearchClient<T> OrderBy(Expression<Func<T, object>> expression)
        {
            return Source.OrderBy(expression);
        }

        public IOrderedSearchClient<T> OrderBy(string propertyName)
        {
            return Source.OrderBy(propertyName);
        }

        public IOrderedSearchClient<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            return Source.OrderByDescending(expression);
        }

        public IOrderedSearchClient<T> OrderByDescending(string propertyName)
        {
            return Source.OrderByDescending(propertyName);
        }

        public Query Query {
            get { return Source.Query; }
        }

        public string Lang {
            get { return Source.Lang; }
            set { }
        }

        public bool IncEmptyFacets {
            get { return Source.IncEmptyFacets; }
            set { }
        }

        public List<SortOrder> SortOrders
        {
            get { return Source.SortOrders; }
        }

        #endregion
        public IOrderedSearchClient<T> ThenBy(Expression<Func<T, object>> expression)
        {
            var propertyName = ExpressionUtils.GetPropertyName(expression);
            return ThenBy(propertyName);
        }

        public IOrderedSearchClient<T> ThenByDescending(Expression<Func<T, object>> expression)
        {
            var propertyName = ExpressionUtils.GetPropertyName(expression);
            return ThenByDescending(propertyName);
        }

        public IOrderedSearchClient<T> ThenBy(string propertyName) {
            AllowedSortTypes<T>.ThrowIfNotSortableType(propertyName);
            SortOrders.Add(new SortOrder{PropertyName = propertyName, Order = Order.Ascending});
            return this;
        }

        public IOrderedSearchClient<T> ThenByDescending(string propertyName) {
            AllowedSortTypes<T>.ThrowIfNotSortableType(propertyName);
            SortOrders.Add(new SortOrder { PropertyName = propertyName, Order = Order.Descending });
            return this;
        }

    }
}
