using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using Seekit.Connection;
using Seekit.Facets;
using Seekit.Linq;
using Seekit.Models;
using Seekit.Settings;

namespace Seekit {
    public class SearchClient<T> : ClientBase where T : SearchModelBase {

        private readonly List<ConvertedExpression> _convertedExpressions = new List<ConvertedExpression>();
        private readonly string _query;
        private bool _includeEmptyFacets;
        public SearchClient(string query = "")
        {
            Configuration = SeekitConfiguration.GetConfiguration();
            _query = query;
        }

        public SearchClient<T> Where(Expression<Func<T, object>> expression) {
            var parser = new ExpressionParser<T>();
            var conExpressions = parser.Parse(expression.Body);
            _convertedExpressions.AddRange(conExpressions);
            return this;
        }

        public SearchClient<T> IncludeEmptyFacets(bool include = true) {
            _includeEmptyFacets = include;
            return this;
        }

        public SearchResult<T> ToList()
        {
            
            var requester = new SearchOperation();
            var jsonData = requester.PreformSearch(JsonConvert.SerializeObject(ConvertExpression()), Configuration);
            var result = JsonConvert.DeserializeObject<SearchResult<T>>(jsonData);;
            if(_includeEmptyFacets)
            {
                var fcm = new FacetContextMerger<T>();
                var facetClient = new FacetsClient();
                var allFacets = facetClient.GetAllFacets<T>(result.CrawlStamp);
                fcm.Merge(result.Facets, allFacets.Facets);
            }
            return result;
        }

        private QueryModel ConvertExpression()
        {
            var expressionConverter = new ExpressionToQuery();
            var queryModel = expressionConverter.Convert(_query, typeof(T), _convertedExpressions);
            queryModel.Client = Configuration.ClientGuid.ToString();
            return queryModel;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ConvertExpression());

        }
    }
}
