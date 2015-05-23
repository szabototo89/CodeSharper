using System;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IRunnable
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        Object Run(Object parameter);
    }

    public interface IRunnable<in TIn, out TOut> : IRunnable
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        TOut Run(TIn parameter);
    }
}