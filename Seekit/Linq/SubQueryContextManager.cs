using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Seekit.Linq {
    public class SubQueryContextManager {

        private List<SubQueryContext> _contexts = new List<SubQueryContext>();
        public static List<string> SupportedMethods = new List<string> { "Any", "WhereAny" };
        public string BaseExpression = string.Empty;


        public void OpenContext(ConvertedExpression expression, BinaryExpression node)
        {
            if (!SupportedMethods.Contains((expression.Equality ?? string.Empty).TrimStart(ExpressionType.Not.ToString().ToCharArray())))
                return;
            _contexts.Add(new SubQueryContext { Target = expression.Target, 
                                                ContextKey = node.ToString(),
                                                // ReSharper disable PossibleNullReferenceException
                                                NegateContext = expression.Equality.StartsWith(ExpressionType.Not.ToString()) 
                                                // ReSharper restore PossibleNullReferenceException
            });

        }

        public void CloseContext(BinaryExpression node)
        {
            if(!_contexts.Any()) return; 

            var last = _contexts.Last();
            if (last.ContextKey == node.ToString()) {
                _contexts.Remove(last);
            }

        }

        internal bool SetContext(ConvertedExpression currentExpression) {
            
            if(!_contexts.Any()) {
                return false;
            }

            currentExpression.Target = _contexts.Last().Target;

            //if it starts with dual "NotNot" its a Not inside the Not context, i guess its an equal then ?
            if (_contexts.Last().NegateContext && currentExpression.Equality.StartsWith(ExpressionType.Not + ExpressionType.Not.ToString())) {
                currentExpression.Equality =
                    currentExpression.Equality.TrimStart((ExpressionType.Not.ToString() + ExpressionType.Not).ToCharArray());
            }
            else if (_contexts.Last().NegateContext && !currentExpression.Equality.StartsWith(ExpressionType.Not.ToString())) {
                currentExpression.Equality = ExpressionType.Not + currentExpression.Equality;
            }


            return true;

        }

        internal void RemoveUnwantedSubQuerys(List<ConvertedExpression> expressions) {
            var subGroups = expressions.Where(x => x.SubQuery != null).GroupBy(x => x.SubQuery);
            if (subGroups.Count() == 0)
                return;

            if (subGroups.Count() == 1 || expressions.Count == 2) {
                expressions.ForEach(x => x.SubQuery = null);
            }

            if (subGroups.Count() == 2 && subGroups.First().Count() != subGroups.Last().Count()) {
                expressions.First().SubQuery = null;
            }
        }

        public bool IsSubQueryExpression(string stringExpression, string equality) {

            var isSupportedMethod = SupportedMethods.Contains((equality ?? string.Empty).TrimStart(ExpressionType.Not.ToString().ToCharArray()));

            if (stringExpression == BaseExpression && !isSupportedMethod)
                return false;

            if ((stringExpression.StartsWith("((") || stringExpression.StartsWith("(" + ExpressionType.Not)) && stringExpression.EndsWith("))"))
                return true;
            return false;


        }

        class SubQueryContext
        {
            public string Target { get; set; }
            public string ContextKey { get; set; }
            public bool NegateContext { get; set; }
        }


    }

    

}
