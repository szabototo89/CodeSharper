using System;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public abstract class ComparisonConstraint<TComparable> : ConstraintBase<TComparable>
        where TComparable : IComparable
    {
        public Predicate<TComparable> Predicate { get; private set; }
        public String Message { get; private set; }

        protected ComparisonConstraint(Predicate<TComparable> predicate, String message)
        {
            Predicate = predicate;
            Message = message;
        }

        protected override void CheckValueAndExpression(TComparable value, String expression)
        {
            if (!Predicate(value))
                ThrowHelper.ThrowArgumentException(expression, Message);
        }
    }
}