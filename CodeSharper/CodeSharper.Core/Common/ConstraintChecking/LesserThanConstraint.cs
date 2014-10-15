using System;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class LesserThanConstraint<TComparable> : ComparisonConstraint<TComparable>
        where TComparable : IComparable
    {
        public LesserThanConstraint(TComparable expectedValue)
            : base(value => value.CompareTo(expectedValue) < 0, String.Format("Argument must be lesser than {0}!", expectedValue))
        {
        }
    }
}