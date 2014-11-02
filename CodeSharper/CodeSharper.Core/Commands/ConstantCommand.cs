using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public class ConstantCommand : CommandBase
    {
        public IRunnable Runnable { get; set; }

        public override IRunnable GetRunnable()
        {
            return Runnable;
        }
    }
}