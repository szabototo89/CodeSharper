using System;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IRunnableWithContext : IRunnable
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter and context
        /// </summary>
        Object Run(Object parameter, Object context);
    }

    public interface IRunnableWithContext<in TIn, out TOut> : IRunnable<TIn, TOut>, IRunnableWithContext
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter and context
        /// </summary>
        TOut Run(TIn parameter, Object context);
    }
}