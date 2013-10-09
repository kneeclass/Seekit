
namespace Seekit.Linq {
    internal class ExpressionValueConverter {
        public object Convert(ConvertedExpression convertedExpression)
        {
            switch (convertedExpression.Equality)
            {
                default:
                    return convertedExpression.Value.ToString();
            }


        }
    }
}
