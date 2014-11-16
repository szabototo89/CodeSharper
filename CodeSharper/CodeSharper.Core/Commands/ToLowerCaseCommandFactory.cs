using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;

namespace CodeSharper.Core.Commands
{
    public class ToLowerCaseCommandFactory : CommandFactoryBase
    {
        private readonly ToLowerCaseRunnable _runnable;

        public ToLowerCaseCommandFactory()
        {
            _runnable = new ToLowerCaseRunnable();
        }

        protected override IRunnable CreateRunnable()
        {
            return _runnable;
        }
    }
}