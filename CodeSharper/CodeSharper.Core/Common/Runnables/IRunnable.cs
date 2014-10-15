using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IRunnable
    {
        IEnumerable<IArgumentWrapper> SupportedArgumentWrappers { get; }
        IEnumerable<IArgumentUnwrapper> SupportedArgumentUnwrappers { get; }

        Object Run(Object parameter);
    }

    public interface IRunnable<in TIn, out TOut> : IRunnable
    {
        TOut Run(TIn parameter);
    }
}
