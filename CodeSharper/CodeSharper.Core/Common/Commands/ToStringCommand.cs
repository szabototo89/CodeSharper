using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Commands
{
    public class ToStringCommand : CommandBase
    {
        public override Argument Execute(Argument parameter)
        {
            if (parameter is IValueArgument)
                return Execute(parameter as IValueArgument);

            return null;
        }

        protected ValueArgument<String> Execute(IValueArgument parameter)
        {
            Constraints
                .NotNull(() => parameter);

            return Arguments.Value(parameter.Value.ToString());
        }
    }
}