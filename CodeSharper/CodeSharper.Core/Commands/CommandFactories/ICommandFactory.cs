namespace CodeSharper.Core.Commands.CommandFactories
{
    public interface ICommandFactory
    {
        CommandDescriptor Descriptor { get; set; }

        ICommand CreateCommand(CommandArgumentCollection arguments);
    }
}