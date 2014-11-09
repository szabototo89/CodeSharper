using System;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Texts;
using Microsoft.CSharp.RuntimeBinder;

namespace CodeSharper.Core.Commands
{
    public abstract class CommandBase : ICommand
    {
        public CommandDescriptor Descriptor { get; protected set; }

        protected CommandBase(CommandDescriptor descriptor)
        {
            Constraints.NotNull(() => descriptor);

            Descriptor = descriptor;
        }

        protected void UpdateArgument<TValue>(ref TValue value, CommandArgumentCollection arguments, String argumentName)
        {
            value = arguments.GetArgumentValue<TValue>(argumentName);
        }

        protected virtual void CreateRunnable() { }

        protected virtual void MapArguments(CommandArgumentCollection arguments)
        {
            foreach (var argument in Descriptor.Arguments)
                if (arguments.All(arg => arg.Name != argument.ArgumentName))
                    arguments.SetArgument(argument.ArgumentName, argument.DefaultValue);
        }

        protected virtual void CheckArguments(CommandArgumentCollection arguments)
        {
            if (!Descriptor
                    .Arguments
                    .Where(arg => !arg.IsOptional)
                    .All(arg => arguments.Any(a => a.Name == arg.ArgumentName)))
                ThrowHelper.ThrowException<InvalidOperationException>();

            if (!Descriptor.Arguments.Join(arguments, arg => arg.ArgumentName, arg => arg.Name,
                (left, right) => left.ArgumentType == right.Value.GetType()).All(element => element))
                ThrowHelper.ThrowException(String.Format("Argument type error of {0}!", Descriptor.Name));
        }

        public virtual void PassArguments(CommandArgumentCollection arguments)
        {
            CheckArguments(arguments);
            MapArguments(arguments);
            CreateRunnable();
        }

        public abstract IRunnable GetRunnable();
    }
}