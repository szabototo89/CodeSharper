using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public class ConstantCommandFactory : CommandFactoryBase
    {
        public IRunnable Runnable { get; set; }
        protected override IRunnable CreateRunnable()
        {
            return Runnable;
        }
    }
}