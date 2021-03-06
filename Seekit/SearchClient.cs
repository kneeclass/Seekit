﻿using System;
using System.Data;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Seekit.Entities;
using Seekit.Extensions;
using Seekit.Linq;
using Seekit.Utils;

namespace Seekit {

    public class SearchClient<T> : SearchClientBase<T>, ISearchClient<T> 
    {
        public SearchClient(string query) : base(query) {
            TypeFullName = typeof (T).JsonNetFormat();
        }
        public SearchClient(string query, string lang) : base(query, lang) {
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

        public IOrderedSearchClient<T> OrderBy(Expression<Func<T, object>> expression) {
            var propertyName = ExpressionUtils.GetPropertyName(expression);
            return OrderBy(propertyName);
        }

        public IOrderedSearchClient<T> OrderBy(string propertyName) {
            AllowedSortTypes<T>.ThrowIfNotSortableType(propertyName);
            ((ISearchClient)this).SortOrders.Add(new SortOrder{PropertyName = propertyName, Order = Order.Ascending});
            return new OrderedSearchClient<T>(this);
        }

        public IOrderedSearchClient<T> OrderByDescending(Expression<Func<T, object>> expression) {
            var propertyName = ExpressionUtils.GetPropertyName(expression);
            return OrderByDescending(propertyName);
        }

        public IOrderedSearchClient<T> OrderByDescending(string propertyName) {
            AllowedSortTypes<T>.ThrowIfNotSortableType(propertyName);
            ((ISearchClient)this).SortOrders.Add(new SortOrder { PropertyName = propertyName, Order = Order.Descending });
            return new OrderedSearchClient<T>(this);
        }

    }
    public class SearchClient : SearchClientBase<SearchModelBase> {
        public SearchClient(string query)
            : base(query) {
        }
        public SearchClient(string query, string lang)
            : base(query, lang) {
        }
        /// <summary>
        /// The number of items to retrive. The default value is 10
        /// </summary>
        /// <param name="count"></param>
        public SearchClient Take(Int32 count) {
            TakeCount = count;
            return this;
        }
        /// <summary>
        /// The number of items to skip. The default value is 0
        /// </summary>
        /// <param name="count"></param>
        public SearchClient Skip(Int32 count) {
            SkipCount = count;
            return this;
        }

    }
}
