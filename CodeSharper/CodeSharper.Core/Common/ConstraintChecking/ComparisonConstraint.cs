using System;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public abstract class ComparisonConstraint : ConstraintBase<Int32>
    {
        public Predicate<Int32> Predicate { get; private set; }
        public String Message { get; private set; }

        protected ComparisonConstraint(Predicate<Int32> predicate, String message)
        {
            Predicate = predicate;
            Message = message;
        }

        protected override void CheckValueAndExpression(Int32 value, String expression)
        {
            if (!Predicate(value))
                ThrowHelper.ThrowArgumentException(expression, Message);
        }
    }
}