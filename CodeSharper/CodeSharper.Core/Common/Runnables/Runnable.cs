using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    public abstract class Runnable<TIn, TOut> : IRunnable, IRunnable<TIn, TOut>
    {
        public abstract TOut Run(TIn parameter);

        Object IRunnable.Run(Object parameter)
        {
            return (TOut)Run((TIn)parameter);
        }
    }
}