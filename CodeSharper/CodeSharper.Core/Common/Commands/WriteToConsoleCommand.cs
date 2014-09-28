using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Commands
{
    public class WriteToConsoleCommand : ValueCommand<String>
    {
        protected override ValueArgument<String> Execute(ValueArgument<String> parameter)
        {
            Constraints
                .NotNull(() => parameter);

            Console.WriteLine(parameter.Value);
            return parameter;
        }
    }
}