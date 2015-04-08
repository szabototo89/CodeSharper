using System;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Common
{
    public interface IExecutor
    {
        /// <summary>
        /// Executes the specified runnable with given parameter.
        /// </summary>
        Object Execute(IRunnable runnable, Object parameter);
    }
}