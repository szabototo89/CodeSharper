using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands
{
    public class CommandArgumentResolver
    {
        private readonly CommandDescriptor _descriptor;
        private readonly CommandArgumentCollection _arguments;

        public CommandArgumentResolver(CommandDescriptor descriptor, CommandArgumentCollection arguments)
        {
            Constraints.NotNull(() => descriptor);
            Constraints.NotNull(() => arguments);

            _descriptor = descriptor;
            _arguments = arguments;
        }

        public CommandArgumentResolver UpdateArgument<TValue>(ref TValue value, String argumentName)
        {
            Constraints.NotBlank(() => argumentName);

            var argumentDescriptor = TryGetArgumentDescriptor(argumentName);
            
            if (argumentDescriptor == Option.None)
                throw new NotSupportedException(string.Format("Not supported argument: {0}!", argumentName));

            var argument = _arguments.TryGetArgument(argumentName);

            if (argument == Option.None && argumentDescriptor.Map(arg => arg.IsOptional))
            {
                value = (TValue) (argumentDescriptor.Value.DefaultValue ?? default(TValue));
                return this;
            }
                
            value = argument.Map(arg => (TValue) arg.Value);
            return this;
        }

        private Option<ArgumentDescriptorBase> TryGetArgumentDescriptor(String argumentName)
        {
            var arguments = _descriptor.Arguments.OfType<NamedArgumentDescriptor>();
            var result = arguments.FirstOrDefault(argument => argument.ArgumentName == argumentName);

            if (result == null)
                return Option.None;

            return Option.Some((ArgumentDescriptorBase)result);
        }
    }
}