using System;

namespace CodeSharper.Core.Common.Runnables
{
    public abstract class RunnableBase<TIn, TOut> : IRunnable, IRunnable<TIn, TOut>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public abstract TOut Run(TIn parameter);

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        Object IRunnable.Run(Object parameter)
        {
            return (TOut)Run((TIn)parameter);
        }
    }
}