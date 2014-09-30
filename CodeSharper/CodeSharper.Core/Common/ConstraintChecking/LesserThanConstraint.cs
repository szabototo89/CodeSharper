using System;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class LesserThanConstraint : ComparisonConstraint
    {
        public LesserThanConstraint(Int32 expectedValue)
            : base(value => value < expectedValue, String.Format("Argument must be lesser than {0}!", expectedValue))
        {
        }
    }
}