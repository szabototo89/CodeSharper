using System;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public abstract class CommandBase : ICommand
    {
        public CommandDescriptor Descriptor { get; set; }

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

            // TODO: Finish this method
        }

        [Initializer]
        protected virtual void InitializeCommand(ArgumentDescriptorBuilder builder)
        {
            Descriptor = new CommandDescriptor()
            {
                Name = GetType().Name,
                Arguments = Enumerable.Empty<ArgumentDescriptor>()
            };
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