using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public interface ICommand
    {
        CommandDescriptor Descriptor { get; }
        IRunnable Runnable { get; }
        CommandArgumentCollection Arguments { get; }
    }
}