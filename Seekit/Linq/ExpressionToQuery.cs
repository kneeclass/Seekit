using System;
using System.Collections.Generic;
using System.Linq;
using Seekit.Connection;
using Seekit.Entities;

namespace Seekit.Linq {
    public class ExpressionToQuery
    {

        public Query Convert(string modelType, List<IQueryPart> queryParts)
        {
            var qm = new Query {ModelType = modelType};

            var convertedExpressions = queryParts.Where(x => x is ConvertedExpression).Cast<ConvertedExpression>().ToList();
            var operators = queryParts.Where(x => x is AndOrOperator).Cast<AndOrOperator>().ToList();
            RewriteValues(convertedExpressions);
            RewriteCondition(operators);
            RewriteEquality(convertedExpressions);
            qm.Filter = queryParts;
            return qm;
        }

        private static void RewriteValues(IEnumerable<ConvertedExpression> convertedExpressions)
        {
            var expressionValueConverter = new ExpressionValueConverter();
            foreach (var convertedExpression in convertedExpressions)
            {
                convertedExpression.Value = expressionValueConverter.Convert(convertedExpression);
            }
        }

        private static void RewriteCondition(IEnumerable<AndOrOperator> andOrOperator)
        {
            foreach (var convertedExpression in andOrOperator)
            {
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
        }
        private static void RewriteEquality(IEnumerable<ConvertedExpression> convertedExpressions)
        {
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
                    case "WithinRadiusOf":
                        convertedExpression.Equality += "WRO";
                        break;
                    case "IsNullOrEmpty":
                        convertedExpression.Equality += "INOE";
                        break;
                    case "IsNullOrWhiteSpace":
                        convertedExpression.Equality += "INOWS";
                        break;
                    default:
                        throw new NotSupportedException(string.Format("The equality operator {0} is not supported",
                                                                      equality));
                }
            }
        }


    }
}
