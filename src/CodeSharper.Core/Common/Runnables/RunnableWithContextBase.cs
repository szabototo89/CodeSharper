using System;

namespace CodeSharper.Core.Common.Runnables
{
    public abstract class RunnableWithContextBase<TIn, TOut> : RunnableBase<TIn, TOut>,
                                                               IRunnableWithContext<TIn, TOut>,
                                                               IRunnableWithContext
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override TOut Run(TIn parameter)
        {
            return Run(parameter, null);
        }

        /// <summary>
        /// Runs an algorithm with the specified parameter and context
        /// </summary>
        public abstract TOut Run(TIn parameter, Object context);

        /// <summary>
        /// Runs an algorithm with the specified parameter and context
        /// </summary>
        public Object Run(Object parameter, Object context)
        {
            return (TOut) Run((TIn) parameter, context);
        }
    }
}