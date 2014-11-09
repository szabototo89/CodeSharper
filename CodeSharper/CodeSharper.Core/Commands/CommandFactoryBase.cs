using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Texts;
using Microsoft.CSharp.RuntimeBinder;

namespace CodeSharper.Core.Commands
{
    public abstract class CommandFactoryBase : ICommandFactory
    {
        protected void UpdateArgument<TValue>(ref TValue value, CommandArgumentCollection arguments, String argumentName)
        {
            value = arguments.GetArgumentValue<TValue>(argumentName);
        }

        protected abstract IRunnable CreateRunnable();

        protected virtual void MapArguments(CommandDescriptor descriptor, CommandArgumentCollection arguments)
        {
            foreach (var argument in descriptor.Arguments.OfType<NamedArgumentDescriptor>())
                if (arguments
                    .All(arg => arg.Name != argument.ArgumentName))
                    arguments.SetArgument(argument.ArgumentName, argument.DefaultValue);
        }

        protected virtual void CheckArguments(CommandDescriptor descriptor, CommandArgumentCollection arguments)
        {
            if (!descriptor
                    .Arguments.OfType<NamedArgumentDescriptor>()
                    .Where(arg => !arg.IsOptional)
                    .All(arg => arguments.Any(a => a.Name == arg.ArgumentName)))
                ThrowHelper.ThrowException<InvalidOperationException>();

            if (!descriptor.Arguments.OfType<NamedArgumentDescriptor>().Join(arguments, arg => arg.ArgumentName, arg => arg.Name,
                (left, right) => left.ArgumentType == right.Value.GetType()).All(element => element))
                ThrowHelper.ThrowException(String.Format("Argument type error of {0}!", descriptor.Name));
        }

        public virtual Command CreateCommand(CommandDescriptor descriptor, CommandArgumentCollection arguments)
        {
            Constraints
                .NotNull(() => descriptor)
                .NotNull(() => arguments);

            CheckArguments(descriptor, arguments);
            MapArguments(descriptor, arguments);
            var runnable = CreateRunnable();
            return new Command(runnable, descriptor, arguments);
        }
    }
}