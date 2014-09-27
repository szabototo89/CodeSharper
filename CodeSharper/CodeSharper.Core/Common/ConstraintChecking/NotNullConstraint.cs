using System;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class NotNullConstraint<TArgumentType>: ConstraintBase<TArgumentType>
    {
        protected override void CheckValueAndExpression(TArgumentType value, String expression)
        {
            if (Equals(value, null))
                ThrowHelper.ThrowArgumentNullException(expression);
        }

        public override event EventHandler<ConstraintEventArgs> ValueChecked;
    }
}