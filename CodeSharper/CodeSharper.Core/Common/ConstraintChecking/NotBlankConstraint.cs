using System;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class NotBlankConstraint : ConstraintBase<String>
    {
        protected override void CheckValueAndExpression(String value, String expression)
        {
            if (string.IsNullOrWhiteSpace(value))
                ThrowHelper.ThrowArgumentException(expression);
        }
    }
}