using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Services;

namespace CodeSharper.Core.Commands
{
    public class DefaultCommandCallResolver : ICommandCallResolver
    {
        /// <summary>
        /// Gets or sets the descriptor repository.
        /// </summary>
        public IDescriptorRepository DescriptorRepository { get; protected set; }

        /// <summary>
        /// Gets or sets the runnable factory.
        /// </summary>
        public IRunnableFactory RunnableFactory { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCommandCallResolver"/> class.
        /// </summary>
        public DefaultCommandCallResolver(IDescriptorRepository descriptorRepository, IRunnableFactory runnableFactory)
        {
            Assume.NotNull(descriptorRepository, "descriptorRepository");
            Assume.NotNull(runnableFactory, "runnableFactory");

            DescriptorRepository = descriptorRepository;
            RunnableFactory = runnableFactory;
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns>Returns created command or null</returns>
        public Command CreateCommand(CommandCallDescriptor descriptor)
        {
            Assume.NotNull(descriptor, "descriptor");

            // get command descriptor from manager
            var commandDescriptors = resolveCommandDescriptorsByName(descriptor);
            var commandDescriptor = resolveCommandDescriptorByParameters(commandDescriptors, descriptor);

            // if there is no proper command descriptor return null
            if (commandDescriptor == null) return null; 
            
            // get actual parameters
            var actualArguments = getActualArguments(commandDescriptor, descriptor);

            // create runnable with factory
            var runnable = RunnableFactory.Create(commandDescriptor.Name, actualArguments);

            // create a command instance
            var command = new Command(runnable, commandDescriptor, actualArguments);

            return command;
        }

        #region Helper methods for creating/getting command

        private Dictionary<String, Object> getActualArguments(CommandDescriptor commandDescriptor, CommandCallDescriptor commandCallDescriptor)
        {
            // order arguments by position
            var positionedArguments = commandCallDescriptor.ActualParameters
                                            .OfType<PositionedCommandCallActualArgument>()
                                            .OrderBy(argument => argument.Position);

            // try to match formal and actual arguments to each other
            var zippedArguments = commandDescriptor.Arguments.Zip(positionedArguments, (formalArg, actualArg) => new {
                FormalArgument = formalArg,
                ActualArgument = actualArg
            });

            // filter not matching types
            zippedArguments = zippedArguments.Where(arguments => {
                if (arguments.ActualArgument.Value == null)
                    return arguments.FormalArgument.ArgumentType == typeof(Object);

                return arguments.FormalArgument.ArgumentType == arguments.ActualArgument.Value.GetType() &&
                       arguments.FormalArgument.Position == arguments.ActualArgument.Position;
            });

            return zippedArguments.ToDictionary(
                arguments => arguments.FormalArgument.ArgumentName,
                arguments => arguments.ActualArgument.Value);
        }

        private CommandDescriptor resolveCommandDescriptorByParameters(IEnumerable<CommandDescriptor> commandDescriptors, CommandCallDescriptor commandCallDescriptor)
        {
            foreach (var commandDescriptor in commandDescriptors)
            {
                // check parameters count equality (note: this class does not handle optional or named parameters
                // if it is not equal jump to the next one
                if (commandDescriptor.Arguments.Count() != commandCallDescriptor.ActualParameters.Count())
                    continue; 

                // get positioned arguments from command call descriptor
                var positionedArguments = commandCallDescriptor.ActualParameters
                    .OfType<PositionedCommandCallActualArgument>()
                    .OrderBy(argument => argument.Position);

                // zip to array into one to find matching argument lists
                var result = commandDescriptor.Arguments.Zip(positionedArguments, (argument, actualArgument) => {
                    if (actualArgument.Value == null)
                        return Tuple.Create(argument.ArgumentType, typeof(Object));
                    return Tuple.Create(argument.ArgumentType, actualArgument.Value.GetType());
                });

                // if there is a match then return with the result
                if (result.All(tuple => tuple.Item1 == tuple.Item2))
                    return commandDescriptor;
            }

            return null;
        }

        private IEnumerable<CommandDescriptor> resolveCommandDescriptorsByName(CommandCallDescriptor descriptor)
        {
            // get all available command descriptors
            var commandDescriptors = DescriptorRepository.GetCommandDescriptors();

            // filter command descriptors by their command name
            var filteredDescriptors = commandDescriptors.Where(commandDescriptor => commandDescriptor.CommandNames.Contains(descriptor.Name));
            return filteredDescriptors.ToList();
        }

        #endregion
    }
}