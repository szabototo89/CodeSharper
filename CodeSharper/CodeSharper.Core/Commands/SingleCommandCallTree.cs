namespace CodeSharper.Core.Commands
{
    public class SingleCommandCallTree : CommandCallTreeBase
    {
        public CommandCallDescriptor CommandCallDescriptor { get; protected set; }

        public SingleCommandCallTree(CommandCallDescriptor descriptor)
        {
            CommandCallDescriptor = descriptor;
        }
    }
}