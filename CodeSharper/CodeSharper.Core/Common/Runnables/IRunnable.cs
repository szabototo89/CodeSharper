using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IRunnable
    {
        IEnumerable<IArgumentConverter> SupportedArgumentConverters { get; }

        Object Run(Object parameter);
    }

    public interface IRunnable<in TIn, out TOut> : IRunnable
    {
        TOut Run(TIn parameter);
    }

    public abstract class Runnable<TIn, TOut> : IRunnable<TIn, TOut>
    {
        private readonly List<IArgumentConverter> _supportedArgumentWrappers; 

        public IEnumerable<IArgumentConverter> SupportedArgumentConverters { get; protected set; }

        protected void RegisterArgumentConverter<TArgumentWrapper>()
            where TArgumentWrapper : IArgumentConverter, new()
        {
            _supportedArgumentWrappers.Add(new TArgumentWrapper());
        }

        protected Runnable()
        {
            _supportedArgumentWrappers = new List<IArgumentConverter>();
        }

        public abstract TOut Run(TIn parameter);

        Object IRunnable.Run(Object parameter)
        {
            return Run((TIn)parameter);
        }
    }
}
