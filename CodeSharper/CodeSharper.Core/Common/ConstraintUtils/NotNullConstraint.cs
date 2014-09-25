using System;
using System.Linq.Expressions;

namespace CodeSharper.Core.Common.ConstraintUtils
{
    public class NotNullConstraint : ConstraintBase<Object>
    {
        public NotNullConstraint(Expression<Func<Object>> expression)
            : base(expression)
        { }

        protected override void CheckValueAndExpression(Object value, String expression)
        {
            if (value == null)
                ThrowHelper.ThrowArgumentNullException(expression);
        }

        public override event EventHandler<ConstraintEventArgs> ValueChecked;
    }
}