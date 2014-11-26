using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class NotEmptyConstraint<TItemType> : ConstraintBase<IEnumerable<TItemType>>
    {
        protected override void CheckValueAndExpression(IEnumerable<TItemType> value, string expression)
        {
            if (!value.Any())
                ThrowHelper.ThrowArgumentException(expression, string.Format("{0} cannot be empty!", expression));
        }
    }
}