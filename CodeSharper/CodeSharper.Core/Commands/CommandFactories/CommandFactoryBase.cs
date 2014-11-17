using System;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public abstract class CommandFactoryBase : ICommandFactory
    {
        public CommandDescriptor Descriptor { get; set; }

        protected void UpdateArgument<TValue>(ref TValue value, CommandArgumentCollection arguments, String argumentName)
        {
            value = arguments.GetArgumentValue<TValue>(argumentName);
        }

        protected abstract IRunnable CreateRunnable();

        protected virtual void MapArguments(CommandArgumentCollection arguments)
        {
            foreach (var argument in Descriptor.Arguments.OfType<ArgumentDescriptor>())
                if (arguments
                    .All(arg => arg.Name != argument.ArgumentName))
                    arguments.SetArgument(argument.ArgumentName, argument.DefaultValue);
        }

        protected virtual void CheckArguments(CommandArgumentCollection arguments)
        {
            if (!Descriptor
                    .Arguments.OfType<ArgumentDescriptor>()
                    .Where(arg => !arg.IsOptional)
                    .All(arg => arguments.Any(a => a.Name == arg.ArgumentName)))
                ThrowHelper.ThrowException<InvalidOperationException>();

            if (!Descriptor.Arguments.OfType<ArgumentDescriptor>().Join(arguments, arg => arg.ArgumentName, arg => arg.Name,
                (left, right) => left.ArgumentType == right.Value.GetType()).All(element => element))
                ThrowHelper.ThrowException(String.Format("Argument type error of {0}!", Descriptor.Name));
        }

        public virtual ICommand CreateCommand(CommandArgumentCollection arguments)
        {
            Constraints
                .NotNull(() => arguments);

            CheckArguments(arguments);
            MapArguments(arguments);
            var runnable = CreateRunnable();
            return new Command(runnable, Descriptor, arguments);
        }
    }
}