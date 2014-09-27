using System;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Common.Values
{
    public class ErrorArgument : Argument
    {
        public ErrorArgument(String message = "")
        {
            Constraints
                .NotNull(() => message);

            Value = message;
        }

        public String Value { get; private set; }
    }
}