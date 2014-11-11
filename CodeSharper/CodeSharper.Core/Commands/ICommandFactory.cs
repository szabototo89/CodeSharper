using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public interface ICommandFactory
    {
        CommandDescriptor Descriptor { get; set; }

        ICommand CreateCommand(CommandArgumentCollection arguments);
    }
}