using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime;

namespace CodeSharper.Core.Common.ConstraintUtils
{
    public abstract class ConstraintBase<TPropertyType> : IConstraint<TPropertyType>
    {
        protected ConstraintBase(Expression<Func<TPropertyType>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<TPropertyType>> Expression { get; private set; }

        protected abstract void CheckValueAndExpression(TPropertyType value, String expression);

        [DebuggerStepThrough]
        public virtual IConstraint Check()
        {
            var expression = ExpressionHelper.GetExpressionName(Expression);
            var value = Expression.Compile()();

            CheckValueAndExpression(value, expression);

            return this;
        }

        protected void OnValueChecked(String expression, Object value)
        {
            var handler = ValueChecked;

            if (handler != null)
                handler(this, new ConstraintEventArgs(expression, value));
        }

        public virtual event EventHandler<ConstraintEventArgs> ValueChecked;
    }
}