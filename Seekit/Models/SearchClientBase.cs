using System;
using System.Collections.Generic;
using Seekit.Facets;
using Seekit.Linq;
using Seekit.Settings;

namespace Seekit.Models {
    public abstract class SearchClientBase : ClientBase {

        protected readonly string QueryTerm;
        internal readonly string Lang;
        protected readonly List<ConvertedExpression> ConvertedExpressions = new List<ConvertedExpression>();
        protected GeoQuery GeoQuery;
        internal bool IncEmptyFacets;

        protected Int32? SkipCount;
        protected Int32? TakeCount;

        protected SearchClientBase(string query)
            : this(query,null)
        {
        }

        protected SearchClientBase(string query, string lang) {
            Configuration = SeekitConfiguration.GetConfiguration();
            QueryTerm = query;
            Lang = string.IsNullOrEmpty(lang) ? null : lang;
        }
        protected string TypeFullName { get; set; }
        private Query _query;
        internal Query Query {
            get {
                if (_query != null)
                    return _query;
                var expressionConverter = new ExpressionToQuery();
                var queryModel = expressionConverter.Convert(QueryTerm, TypeFullName, ConvertedExpressions,Lang);
                queryModel.GeoQuery = GeoQuery;
                queryModel.Take = TakeCount.GetValueOrDefault(10);
                queryModel.Skip = SkipCount.GetValueOrDefault(0);
                _query = queryModel;
                return queryModel;
            }
        }
        protected QueryContext ConvertExpression() {
            var context = new QueryContext {
                Client = Configuration.ClientGuid.ToString(), 
            };
            context.Querys.Add(Query);
            return context;
        }

        internal abstract void MergeFacets<T>(string crawlStamp, FacetsList<T> facetsList, Type typeOverride = null);
        internal abstract Type ModelType { get; }
    }
}
