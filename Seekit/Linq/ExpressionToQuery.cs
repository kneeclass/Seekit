using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seekit.Connection;

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
                switch (convertedExpression.Condition)
                {
                    case "OrElse":
                        convertedExpression.Condition = "OR";
                        continue;
                    case "AndAlso":
                    case null:
                        convertedExpression.Condition = "AND";
                        continue;
                }


            }

        }
        private static void RewriteEquality(IEnumerable<ConvertedExpression> convertedExpressions) {
            foreach (var convertedExpression in convertedExpressions) {
                switch (convertedExpression.Equality) {
                    case "NotEqual":
                        convertedExpression.Equality = "NEQ";
                        break;
                    case "Equal":
                        convertedExpression.Equality = "EQ";
                        break;
                    case "GreaterThan":
                        convertedExpression.Equality = "GT";
                        break;
                    case "GreaterThanOrEqual":
                        convertedExpression.Equality = "GTOEQ";
                        break;
                    case "LessThan":
                        convertedExpression.Equality = "LT";
                        break;
                    case "LessThanOrEqual":
                        convertedExpression.Equality = "LTOEQ";
                        break;
                    case "StartsWith":
                        convertedExpression.Equality = "SW";
                        break;
                    case "EndsWith":
                        convertedExpression.Equality = "EW";
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
