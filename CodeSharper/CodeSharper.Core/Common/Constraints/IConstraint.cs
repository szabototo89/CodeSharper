using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common.Constraints
{
    public interface IConstraint<TExpression>
    {
        Expression<Func<TExpression>> Expression { get; }

        IConstraint<TExpression> Check();

        event EventHandler<EventArgs> ValueChecked;
    }

    public class NullReferenceConstraint : IConstraint<object>
    {
        public NullReferenceConstraint(Expression<Func<object>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<object>> Expression { get; private set; }

        public IConstraint<object> Check()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<EventArgs> ValueChecked;
    }
}
