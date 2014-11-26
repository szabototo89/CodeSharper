using System;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class EvaluateConstraint<TArgumentType> : ConstraintBase<TArgumentType>
    {
        private readonly Predicate<TArgumentType> _predicate;
        private readonly string _message;

        public EvaluateConstraint(Predicate<TArgumentType> predicate, String message = "")
        {
            _predicate = predicate;
            _message = message;
        }

        protected override void CheckValueAndExpression(TArgumentType value, String expression)
        {
            if (!_predicate(value))
                ThrowHelper.ThrowArgumentException(expression, _message);
        }
    }
}