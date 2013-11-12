
using System;
using System.Linq;

namespace Seekit.Linq {
    internal class ExpressionValueConverter {
        public object Convert(ConvertedExpression convertedExpression)
        {
            if (convertedExpression.TargetType.IsEnum) {
                return Enum.GetName(convertedExpression.TargetType, convertedExpression.Value);
            }
            return convertedExpression.Value;
        }
    }
}
