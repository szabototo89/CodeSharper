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
    public class StandardCommandManager
    {
        private readonly Dictionary<CommandDescriptor, ICommandFactory> _commands;

        public StandardCommandManager()
        {
            _commands = new Dictionary<CommandDescriptor, ICommandFactory>();
        }

        public StandardCommandManager RegisterCommand(ICommandFactory commandFactory)
        {
            return this;
        }

        public Option<ICommandFactory> TryGetCommand(CommandCallDescriptor callDescriptor)
        {
            Constraints.NotNull(() => callDescriptor);

            var command = TryGetCommandsByName(callDescriptor.Name).SingleOrDefault();

            if (command == null)
                return Option.None;

            var arguments = new CommandArgumentCollection();
            foreach (var argument in callDescriptor.NamedArguments)
                arguments.SetArgument(argument.Key, argument.Value);

            //command.PassArguments(arguments);
            return Option.Some(command);
        }

        public IEnumerable<ICommandFactory> TryGetCommandsByName(String name)
        {
            return _commands.Where(pair => pair.Key.CommandNames.Any(command => String.Equals(command, name)))
                .Select(pair => pair.Value);
        }
    }
}
