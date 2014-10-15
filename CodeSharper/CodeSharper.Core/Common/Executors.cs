using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common
{
    public static class Executors
    {
        public static StandardExecutor<TIn, TOut> CreateStandardExecutor<TIn, TOut>(IRunnable<TIn, TOut> runnable)
        {
            Constraints.NotNull(() => runnable);
            return new StandardExecutor<TIn, TOut>(runnable);
        }
    }
}
