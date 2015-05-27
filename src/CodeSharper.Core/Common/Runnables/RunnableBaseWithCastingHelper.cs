using System;

namespace CodeSharper.Core.Common.Runnables
{
    public abstract class RunnableBase<TIn, TOut, TCastingHelper> : IRunnable<TIn, TOut>
        where TCastingHelper : ICastingHelper<TIn>, new()
    {
        protected readonly TCastingHelper CastingHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunnableBase{TIn, TOut, TCastingHelper}"/> class.
        /// </summary>
        protected RunnableBase()
        {
            CastingHelper = new TCastingHelper();
        }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public abstract TOut Run(TIn parameter);

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        Object IRunnable.Run(Object parameter)
        {
            return Run(CastingHelper.Cast(parameter));
        }
    }
}