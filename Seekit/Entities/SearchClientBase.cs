using System;
using System.Collections.Generic;
using Seekit.Linq;
using Seekit.Settings;

namespace Seekit.Entities {
    public abstract class SearchClientBase : ClientBase, ISearchClient {

        protected readonly string QueryTerm;
        protected readonly List<ConvertedExpression> ConvertedExpressions = new List<ConvertedExpression>();
        protected GeoQuery GeoQuery;

        protected Int32? SkipCount;
        protected Int32? TakeCount;

        protected SearchClientBase(string query)
            : this(query,null)
        {
        }

        protected SearchClientBase(string query, string lang) {
            Configuration = SeekitConfiguration.GetConfiguration();
            QueryTerm = query;
            _sortOrder = new List<SortOrder>();
            ((ISearchClient)this).Lang = string.IsNullOrEmpty(lang) ? null : lang;
        }
        protected string TypeFullName { get; set; }
        private Query _query;

        Query ISearchClient.Query {
            get {
                if (_query != null)
                    return _query;
                var expressionConverter = new ExpressionToQuery();
                var queryModel = expressionConverter.Convert(TypeFullName, ConvertedExpressions);
                queryModel.SearchTerms = QueryTerm;
                queryModel.Lang = ((ISearchClient) this).Lang;
                queryModel.GeoQuery = GeoQuery;
                queryModel.Take = TakeCount.GetValueOrDefault(10);
                queryModel.Skip = SkipCount.GetValueOrDefault(0);
                queryModel.SortOrders = _sortOrder;
                _query = queryModel;
                return queryModel;
            }
        }
        string ISearchClient.Lang { get; set; }
        bool ISearchClient.IncEmptyFacets { get; set; }

        private readonly List<SortOrder> _sortOrder;
        List<SortOrder> ISearchClient.SortOrders
        {
            get { return _sortOrder; }
        }

        protected QueryContext ConvertExpression() {
            var context = new QueryContext {
                Client = Configuration.ClientGuid.ToString(), 
            };
            context.Querys.Add(((ISearchClient)this).Query);
            return context;
        }

    }
}
