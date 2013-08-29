using System;
using System.Collections.Generic;
using System.Linq;
using Seekit.Connection;
using Seekit.Models;

namespace Seekit.Linq {
    public class ExpressionToQuery
    {

        public QueryModel Convert(string query, Type modelType, List<ConvertedExpression> convertedExpressions) {
            var qm = new QueryModel();
            qm.ModelType = modelType.FullName;
            qm.Query = query;

            RewriteCondition(convertedExpressions);
            RewriteEquality(convertedExpressions);
            qm.Filter = convertedExpressions;
            return qm;
        }

        private static void RewriteCondition(IEnumerable<ConvertedExpression> convertedExpressions) {
            foreach (var convertedExpression in convertedExpressions) {
                switch (convertedExpression.Operator)
                {
                    case "OrElse":
                        convertedExpression.Operator = "OR";
                        continue;
                    case "AndAlso":
                    case null:
                        convertedExpression.Operator = "AND";
                        continue;
                }
            }
            if (convertedExpressions.Any()){
                convertedExpressions.First().Operator = null;
            }

        }
        private static void RewriteEquality(IEnumerable<ConvertedExpression> convertedExpressions) {
            foreach (var convertedExpression in convertedExpressions) {
                var equality = convertedExpression.Equality;
                convertedExpression.Equality = string.Empty;

                if (equality.StartsWith("Not")) {
                    convertedExpression.Equality = "N:";
                    equality = equality.TrimStart("Not".ToCharArray());
                }

                foreach (var method in SubQueryContextManager.SupportedMethods) {
                    if(equality.StartsWith(method)) {
                        equality = equality.TrimStart(method.ToCharArray());
                        break;
                    }
                }

                switch (equality) {
                    case "Equal":
                    case "Equals":
                    case "Contains":
                        convertedExpression.Equality += "EQ";
                        break;
                    case "GreaterThan":
                        convertedExpression.Equality += "GT";
                        break;
                    case "GreaterThanOrEqual":
                        convertedExpression.Equality += "GTOEQ";
                        break;
                    case "LessThan":
                        convertedExpression.Equality += "LT";
                        break;
                    case "LessThanOrEqual":
                        convertedExpression.Equality += "LTOEQ";
                        break;
                    case "StartsWith":
                        convertedExpression.Equality += "SW";
                        break;
                    case "EndsWith":
                        convertedExpression.Equality += "EW";
                        break;
                    default:
                        convertedExpression.Equality += equality;
                        break;
                }
            }
        }


    }
}
