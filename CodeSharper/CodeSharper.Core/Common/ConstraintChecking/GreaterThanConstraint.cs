using System;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class GreaterThanConstraint : ComparisonConstraint
    {
        public GreaterThanConstraint(Int32 expectedValue) 
            : base(value => value > expectedValue, String.Format("Argument must be greater than {0}!", expectedValue))
        {
        }
    }
}