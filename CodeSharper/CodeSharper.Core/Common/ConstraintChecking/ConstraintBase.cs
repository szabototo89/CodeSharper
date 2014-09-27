using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public abstract class ConstraintBase<TPropertyType> : IConstraint<TPropertyType>
    {
        protected abstract void CheckValueAndExpression(TPropertyType value, String expression);

        [DebuggerStepThrough]
        public virtual IConstraint<TPropertyType> Check(Expression<Func<TPropertyType>> expression)
        {
            var expressionName = ExpressionHelper.GetExpressionName(expression);
            var value = expression.Compile()();

            CheckValueAndExpression(value, expressionName);

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