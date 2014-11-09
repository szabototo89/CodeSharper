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
    public class CommandBuilder
    {
        private readonly Dictionary<CommandDescriptor, IRunnable> _runnables;

        public CommandBuilder()
        {
            _runnables = new Dictionary<CommandDescriptor, IRunnable>();
        }

        public void RegisterRunnable(IRunnable runnable, CommandDescriptor commandDescriptor)
        {
            Constraints
                .NotNull(() => runnable)
                .NotNull(() => commandDescriptor);

            _runnables.Add(commandDescriptor, runnable);
        }


        public IEnumerable<IRunnable> TryGetRunnablesByName(String name)
        {
            return _runnables.Where(pair => pair.Key.CommandNames.Any(command => String.Equals(command, name)))
                .Select(pair => pair.Value);
        }
    }
}
