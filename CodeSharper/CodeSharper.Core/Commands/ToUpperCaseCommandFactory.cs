using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;

namespace CodeSharper.Core.Commands
{
    public class ToUpperCaseCommandFactory : CommandFactoryBase
    {
        private readonly ToUpperCaseRunnable _runnable;

        public ToUpperCaseCommandFactory() 
        {
            _runnable = new ToUpperCaseRunnable();
        }

        protected override IRunnable CreateRunnable()
        {
            return _runnable;
        }
    }

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