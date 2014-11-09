using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public class Command : ICommand
    {
        public Command(IRunnable runnable, CommandDescriptor descriptor, CommandArgumentCollection arguments)
        {
            Runnable = runnable;
            Descriptor = descriptor;
            Arguments = arguments;
        }

        public CommandDescriptor Descriptor { get; private set; }

        public IRunnable Runnable { get; private set; }

        public CommandArgumentCollection Arguments { get; private set; }
    }
}