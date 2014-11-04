using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public interface ICommand
    {
        void PassArguments(CommandArgumentCollection arguments);

        IRunnable GetRunnable();

        CommandDescriptor Descriptor { get; }
    }
}