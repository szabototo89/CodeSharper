using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public class ConstantCommand : CommandBase
    {
        public ConstantCommand(CommandDescriptor descriptor) : base(descriptor)
        {
        }

        public IRunnable Runnable { get; set; }

        public override IRunnable GetRunnable()
        {
            return Runnable;
        }
    }
}