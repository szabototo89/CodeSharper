using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Commands
{
    public class ComposeCommand : CommandBase
    {
        private readonly IEnumerable<ICommand> _commands;

        public ComposeCommand() : this(Enumerable.Empty<ICommand>().ToArray()) { }

        public ComposeCommand(params ICommand[] commands)
        {
            Constraints.NotNull(() => commands);

            _commands = commands;
        }

        public override Argument Execute(Argument parameter)
        {
            Argument argument = parameter;
            foreach (var command in _commands)
                argument = command.Execute(argument);

            return argument;
        }
    }
}