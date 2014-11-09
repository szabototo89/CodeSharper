using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public interface ICommandFactory
    {
        Command CreateCommand(CommandDescriptor descriptor, CommandArgumentCollection arguments);
    }
}