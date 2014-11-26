using System;
using System.Diagnostics;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class NotNullConstraint<TArgumentType>: ConstraintBase<TArgumentType>
    {
        [DebuggerStepThrough]
        protected override void CheckValueAndExpression(TArgumentType value, String expression)
        {
            if (Equals(value, null))
                ThrowHelper.ThrowArgumentNullException(expression);
        }

        public override event EventHandler<ConstraintEventArgs> ValueChecked;
    }
}