using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Seekit.Facets;

namespace Seekit.Linq {
    public class ExpressionParser<T> : ExpressionVisitor {
        private readonly Queue<string> _conditionalQueue;
        private ConvertedExpression _currentExpression;
        private SubQueryContextManager _subQueryContext;
        private readonly List<ConvertedExpression> _expressions;


        public List<ConvertedExpression> Parse(Expression expression) {
            var e = Evaluator.PartialEval(expression);
            Visit(e);

            _subQueryContext.RemoveUnwantedSubQuerys(_expressions);

            return _expressions;
        }

        private ConvertedExpression GetCurrentExpression()
        {
            return _currentExpression ?? (_currentExpression = new ConvertedExpression());
        }

        private void FinalizeCurrentExpression() {

            if (_expressions.Any()) {
                _currentExpression.Operator = _conditionalQueue.Dequeue();
            }

            _subQueryContext.SetContext(_currentExpression);

            _expressions.Add(_currentExpression);
            _currentExpression = null;
        }


        public ExpressionParser() {
            _conditionalQueue = new Queue<string>();
            _expressions = new List<ConvertedExpression>();
            _subQueryContext = new SubQueryContextManager();
        }


        private bool _visitUnaryVisited;
        protected override Expression VisitUnary(UnaryExpression node) {

            if (!_visitUnaryVisited) {
                _subQueryContext.BaseExpression = node.Operand.ToString();
                _visitUnaryVisited = true;
            }

            if (node.NodeType == ExpressionType.Not) {
                var conexpress = GetCurrentExpression();
                conexpress.Equality = node.NodeType + conexpress.Equality ?? string.Empty;
            }
            return base.VisitUnary(node);
        }
        protected override Expression VisitMethodCall(MethodCallExpression node) {

            if (node.Method.DeclaringType == typeof(string) && node.Method.Name == "Contains") {
                throw new NotSupportedException("The string method Contains is not supported by Seekit: " + node);
            }
            var conexpress = GetCurrentExpression();

            if (IsDoubleNegetate(conexpress, node.Method.Name)) {
                conexpress.Equality = ExpressionType.Not + conexpress.Equality +
                                              node.Method.Name.TrimStart(ExpressionType.Not.ToString().ToCharArray());
            }
            else {
                conexpress.Equality += node.Method.Name;
            }

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

                    //this happens if a subquery has a "NOTed" the method ex: !Any(x=> x != "asd")
                    if (IsDoubleNegetate(conexpress, node.NodeType.ToString())) {
                        //make it a double NotNot so that the SubQueryContextManager can pick this up in the Fill method
                        conexpress.Equality = ExpressionType.Not + conexpress.Equality +
                                              node.NodeType.ToString().TrimStart(ExpressionType.Not.ToString().ToCharArray());
                    }
                    else {
                        conexpress.Equality += node.NodeType.ToString();
                    }
                    break;
            }

            var strExp = node.ToString();
            var expression = GetCurrentExpression();
            var equality = GetCurrentExpression().Equality;
            if (_subQueryContext.IsSubQueryExpression(strExp, equality)) {
                expression.SubQuery = SubQuery.Open;
                _subQueryContext.OpenContext(expression, node);
            }

            var retExpression = base.VisitBinary(node);

            if (_subQueryContext.IsSubQueryExpression(strExp, equality)) {
                _expressions.Last().SubQuery = SubQuery.Close;
                _subQueryContext.CloseContext(node);
            }
            return retExpression;
        }
        protected override Expression VisitConstant(ConstantExpression node) {

            var conexpress = GetCurrentExpression();
            conexpress.Value.SetValue(node.Value);
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

        private static bool IsDoubleNegetate(ConvertedExpression convertedExpression, string newEquality) {

            if (string.IsNullOrEmpty(convertedExpression.Equality))
                return false;

            return convertedExpression.Equality.StartsWith(ExpressionType.Not.ToString()) && newEquality.StartsWith(ExpressionType.Not.ToString());
        }

    }
}
