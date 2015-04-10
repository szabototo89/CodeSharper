using System;

namespace CodeSharper.Core.Common.Runnables
{
    public abstract class SafeRunnableBase<TIn, TOut> : RunnableBase<TIn, TOut>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override Object Run(Object parameter)
        {
            try
            {
                return base.Run(parameter);
            }
            catch (Exception exception)
            {
                return new ErrorMessage(exception);
            }
        }
    }
}