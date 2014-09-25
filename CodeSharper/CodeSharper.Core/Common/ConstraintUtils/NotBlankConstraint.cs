using System;
using System.Linq.Expressions;

namespace CodeSharper.Core.Common.ConstraintUtils
{
    public class NotBlankConstraint : ConstraintBase<String>
    {
        public NotBlankConstraint(Expression<Func<String>> func)
            : base(func) { }

        protected override void CheckValueAndExpression(String value, String expression)
        {
            if (string.IsNullOrWhiteSpace(value))
                ThrowHelper.ArgumentException(expression);
        }
    }
}