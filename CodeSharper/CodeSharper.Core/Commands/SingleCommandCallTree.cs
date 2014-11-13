namespace CodeSharper.Core.Commands
{
    public class SingleCommandCall : CommandCallBase
    {
        public CommandCallDescriptor CommandCallDescriptor { get; protected set; }

        public SingleCommandCall(CommandCallDescriptor descriptor)
        {
            CommandCallDescriptor = descriptor;
        }
    }
}