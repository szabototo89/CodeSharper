using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Commands
{
    public class DefaultCommandCallResolver : ICommandCallResolver
    {
        /// <summary>
        /// Gets or sets the runnable factory.
        /// </summary>
        public IRunnableFactory RunnableFactory { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCommandCallResolver"/> class.
        /// </summary>
        public DefaultCommandCallResolver(IRunnableFactory runnableFactory)
        {
            Assume.NotNull(runnableFactory, "runnableFactory");

            RunnableFactory = runnableFactory;
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        public Command CreateCommand(CommandCallDescriptor descriptor)
        {
            Assume.NotNull(descriptor, "descriptor");

            throw new NotImplementedException();

            // var commandDescriptor = getCommandDescriptor(descriptor);
            // var runnable = RunnableFactory.Create(descriptor.Name, actualArguments);
        }
    }
}