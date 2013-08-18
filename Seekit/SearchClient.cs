﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using Seekit.Connection;
using Seekit.Linq;

namespace Seekit {
    public class SearchClient<T> where T : SearchModelBase {

        public List<ConvertedExpression> ConvertedExpressions = new List<ConvertedExpression>();
        private string _query;
        public SearchClient(string query = "")
        {
            _query = query;
        }

        public SearchClient<T> Where(Expression<Func<T, object>> expression) {
            var parser = new ExpressionParser<T>();
            var conExpressions = parser.Parse(expression);
            ConvertedExpressions.AddRange(conExpressions);
            return this;
        }

        public SearchResult<T> ToList()
        {
            var expressionConverter = new ExpressionToQuery();
            var queryModel = expressionConverter.Convert(_query, typeof (T), ConvertedExpressions);

            queryModel.Client = "d065b8fc-2930-417c-9a8c-19df62a7bb9a";
            var requester = new SearchOperations();
            var jsonData = requester.PreformSearch(JsonConvert.SerializeObject(queryModel));
            jsonData = jsonData.Replace("created", "created2");
            return JsonConvert.DeserializeObject<SearchResult<T>>(jsonData);
        }

        public override string ToString()
        {
            var expressionConverter = new ExpressionToQuery();
            return JsonConvert.SerializeObject(expressionConverter.Convert(_query, typeof(T), ConvertedExpressions));

        }
    }
}
