using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Seekit.Linq {
    public class ExpressionParser<T> : ExpressionVisitor {
        private readonly Queue<string> _conditionalQueue;
        private ConvertedExpression _currentExpression;
        private readonly List<ConvertedExpression> _expressions;
        private string _baseExpression = string.Empty;


        public List<ConvertedExpression> Parse(Expression expression) {
            var e = Evaluator.PartialEval(expression);
            Visit(e);
            return _expressions;
        }

        private ConvertedExpression GetCurrentExpression() {
            if (_currentExpression == null) {
                _currentExpression = new ConvertedExpression();
            }
            return _currentExpression;
        }
        private void FinalizeCurrentExpression() {

            if (_expressions.Any()) {
                _currentExpression.Condition = _conditionalQueue.Dequeue();
            }
            _expressions.Add(_currentExpression);
            _currentExpression = null;
        }


        public ExpressionParser() {
            _conditionalQueue = new Queue<string>();
            _expressions = new List<ConvertedExpression>();
        }


        private bool _visitUnaryVisited;
        protected override Expression VisitUnary(UnaryExpression node) {

            if(!_visitUnaryVisited) {
                _baseExpression = node.Operand.ToString();
                _visitUnaryVisited = true;
            }

            if (node.NodeType == ExpressionType.Not) {
                var conexpress = GetCurrentExpression();
                conexpress.Equality += node.NodeType.ToString();
            }
            return base.VisitUnary(node);
        }
        protected override Expression VisitMethodCall(MethodCallExpression node) {

            var conexpress = GetCurrentExpression();
            conexpress.Equality += node.Method.Name;

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node) {

            switch (node.NodeType.ToString()) {
                case "OrElse":
                case "AndAlso":
                    _conditionalQueue.Enqueue(node.NodeType.ToString());
                    break;
                default:
                    var conexpress = GetCurrentExpression();
                    conexpress.Equality += node.NodeType.ToString();
                    break;
            }

            var strExp = node.ToString();

            if (strExp != _baseExpression && strExp.StartsWith("((") && strExp.EndsWith("))")) {
                GetCurrentExpression().SubQuery = SubQuery.Open;
            }

            var retExpression = base.VisitBinary(node);

            if (strExp != _baseExpression && strExp.StartsWith("((") && strExp.EndsWith("))"))
            {
                _expressions.Last().SubQuery = SubQuery.Close;
            }
            return retExpression;
        }
        protected override Expression VisitConstant(ConstantExpression node) {
            
                var conexpress = GetCurrentExpression();
                conexpress.Value = node.Value;
                FinalizeCurrentExpression();
            
            return base.VisitConstant(node);
        }
        protected override Expression VisitMember(MemberExpression node) {
            if (node.Member.DeclaringType.UnderlyingSystemType == typeof(T)) {
                var conexpress = GetCurrentExpression();
                conexpress.Target = node.Member.Name;
            }
            return base.VisitMember(node);
        }

    }
}
