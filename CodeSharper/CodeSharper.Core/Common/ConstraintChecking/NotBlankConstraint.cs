using System;
using System.Diagnostics;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class NotBlankConstraint : ConstraintBase<String>
    {
        [DebuggerStepThrough]
        protected override void CheckValueAndExpression(String value, String expression)
        {
            if (string.IsNullOrWhiteSpace(value))
                ThrowHelper.ThrowArgumentException(expression);
        }
    }
}