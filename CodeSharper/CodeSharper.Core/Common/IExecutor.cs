using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public interface IExecutor
    {
        Argument Execute(IRunnable runnable, Argument parameter);
    }
}