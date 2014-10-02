using System;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class GreaterThanConstraint<TComparable> : ComparisonConstraint<TComparable>
        where TComparable : IComparable
    {
        public GreaterThanConstraint(TComparable expectedValue)
            : base(value => value.CompareTo(expectedValue) > 0, String.Format("Argument must be greater than {0}!", expectedValue))
        {
        }
    }
}