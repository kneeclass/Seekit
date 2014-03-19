using System.Collections.Generic;
using System.Linq.Expressions;
using Seekit.Entities;

namespace Seekit.Linq
{
    public interface IExpressionParser<T>
    {
        List<IQueryPart> Parse(Expression expression);
    }
}
