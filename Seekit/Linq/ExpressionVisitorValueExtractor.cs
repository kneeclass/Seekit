using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Seekit.Entities;

namespace Seekit.Linq
{
    public class ExpressionVisitorValueExtractor<T> : ExpressionVisitor {
        private readonly List<IQueryPart> _convertedExpressions;
        private ConvertedExpression _expression;

        public ExpressionVisitorValueExtractor(List<IQueryPart> convertedExpressions)
        {
            _convertedExpressions = convertedExpressions;
            _expression = new ConvertedExpression();
            _convertedExpressions.Add(_expression);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {

            _expression.Value = node.Value;
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node) {
            _expression.Equality += node.Method.Name;
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType.UnderlyingSystemType == typeof(T)) {
                _expression.Target = node.Member.Name;
                _expression.TargetType = node.Type;
            }
            return base.VisitMember(node);
        }
    }
}
