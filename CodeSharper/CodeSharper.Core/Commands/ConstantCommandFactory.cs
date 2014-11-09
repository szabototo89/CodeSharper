using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public class ConstantCommandFactory : CommandFactoryBase
    {
        public ConstantCommandFactory() { }

        public IRunnable Runnable { get; set; }
        protected override IRunnable CreateRunnable()
        {
            return Runnable;
        }
    }
}