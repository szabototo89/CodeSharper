using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands
{
    public class StandardCommandManager : ICommandManager
    {
        private readonly Dictionary<CommandDescriptor, ICommandFactory> _commands;

        public StandardCommandManager()
        {
            _commands = new Dictionary<CommandDescriptor, ICommandFactory>();
        }

        public StandardCommandManager RegisterCommandFactory(ICommandFactory commandFactory)
        {
            Constraints
                .NotNull(() => commandFactory)
                .NotNull(() => commandFactory.Descriptor);

            _commands.Add(commandFactory.Descriptor, commandFactory);

            return this;
        }

        public Option<ICommand> TryGetCommand(CommandCallDescriptor callDescriptor)
        {
            Constraints.NotNull(() => callDescriptor);

            var factory = TryGetCommandFactoriesByName(callDescriptor.Name).SingleOrDefault();

            if (factory == null)
                return Option.None;

            var arguments = new CommandArgumentCollection();
            foreach (var argument in callDescriptor.NamedArguments)
                arguments.SetArgument(argument.Key, argument.Value);

            return Option.Some(factory.CreateCommand(arguments));
        }

        public IEnumerable<ICommandFactory> TryGetCommandFactoriesByName(String name)
        {
            return _commands.Where(pair => pair.Key.CommandNames.Any(command => String.Equals(command, name)))
                .Select(pair => pair.Value);
        }
    }
}
