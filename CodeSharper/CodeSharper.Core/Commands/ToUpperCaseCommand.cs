using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;

namespace CodeSharper.Core.Commands
{
    public class ToUpperCaseCommand : CommandBase
    {
        private readonly ToUpperCaseRunnable _runnable;

        public ToUpperCaseCommand()
        {
            _runnable = new ToUpperCaseRunnable();
        }

        public override IRunnable GetRunnable()
        {
            return _runnable;
        }
    }
}