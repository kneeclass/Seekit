using System;
using System.Linq.Expressions;

namespace Seekit.Utils {
    public class ExpressionUtils {

        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            MemberExpression body = null;
            if (expression.Body is MemberExpression) {
                body = (MemberExpression)expression.Body;
            }
            else if (expression.Body is UnaryExpression) {
                var expression3 = (UnaryExpression)expression.Body;
                body = expression3.Operand as MemberExpression;
            }
            if (body == null) {
                throw new Exception("The body of the expression must be either a MemberExpression of a UnaryExpression.");
            }
            return body.Member.Name;


        }

    }
}
