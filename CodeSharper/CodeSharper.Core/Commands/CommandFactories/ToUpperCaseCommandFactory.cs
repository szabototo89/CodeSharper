using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;

namespace CodeSharper.Core.Commands.CommandFactories
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
}