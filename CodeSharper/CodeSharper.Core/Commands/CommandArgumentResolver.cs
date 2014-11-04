using System;
using System.Runtime.Remoting.Messaging;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Commands
{
    public class CommandArgumentResolver
    {
        private readonly CommandArgumentCollection _arguments;

        public CommandArgumentResolver(CommandArgumentCollection arguments)
        {
            _arguments = arguments;
        }

        public CommandArgumentResolver UpdateArgument<TValue>(ref TValue value, String argumentName)
        {
            Constraints.NotBlank(() => argumentName);
            value = _arguments.GetArgument<TValue>(argumentName);

            return this;
        }
    }
}