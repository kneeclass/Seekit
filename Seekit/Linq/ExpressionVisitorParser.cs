using System.Collections.Generic;
using System.Linq.Expressions;
using Seekit.Entities;

namespace Seekit.Linq
{
    public class ExpressionVisitorParser<T> :ExpressionVisitor, IExpressionParser<T>
    {
        private List<IQueryPart> _convertedExpressions;
        public List<IQueryPart> Parse(Expression expression)
        {
            var e = Evaluator.PartialEval(expression);
            _convertedExpressions = new List<IQueryPart>();
            Visit(e);
            return _convertedExpressions;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (!(node.Operand is BinaryExpression)) {
                var visitor = new ExpressionVisitorValueExtractor<T>(_convertedExpressions);
                visitor.Visit(node.Operand);
            }
            return base.VisitUnary(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.AndAlso || node.NodeType == ExpressionType.OrElse) {
                var visitorLeft = new ExpressionVisitorValueExtractor<T>(_convertedExpressions);
                visitorLeft.Visit(node.Left);
                _convertedExpressions.Add(new AndOrOperator{Operator = node.NodeType.ToString()});
                var visitorRight = new ExpressionVisitorValueExtractor<T>(_convertedExpressions);
                visitorRight.Visit(node.Right);
                return node;
            }
            var visitor = new ExpressionVisitorValueExtractor<T>(_convertedExpressions);
            visitor.Visit(node);

            return node;
        }
    }
}
