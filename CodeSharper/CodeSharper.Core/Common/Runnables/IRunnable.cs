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
        IEnumerable<IArgumentConverter> SupportedArgumentWrappers { get; }
        IEnumerable<IArgumentConverter> SupportedArgumentUnwrappers { get; }

        Object Run(Object parameter);
    }

    public interface IRunnable<in TIn, out TOut> : IRunnable
    {
        TOut Run(TIn parameter);
    }

    // TODO: Refactor this section (Consumes/Produces) to another static class
    public abstract class Runnable<TIn, TOut> : IRunnable<TIn, TOut>
    {
        private readonly List<IArgumentConverter> _supportedArgumentWrappers;
        private readonly List<IArgumentConverter> _supportedArgumentUnwrappers; 

        public IEnumerable<IArgumentConverter> SupportedArgumentWrappers { get { return _supportedArgumentWrappers.AsReadOnly(); } }

        public IEnumerable<IArgumentConverter> SupportedArgumentUnwrappers { get { return _supportedArgumentUnwrappers.AsReadOnly(); } }

        protected void Consumes<TArgumentWrapper>()
            where TArgumentWrapper : IArgumentConverter, new()
        {
            _supportedArgumentWrappers.Add(new TArgumentWrapper());
        }

        protected void Produces<TArgumentUnwrapper>() 
            where TArgumentUnwrapper : IArgumentConverter, new()

        {
            _supportedArgumentUnwrappers.Add(new TArgumentUnwrapper());
        }
        protected Runnable()
        {
            _supportedArgumentWrappers = new List<IArgumentConverter>();
            _supportedArgumentUnwrappers =  new List<IArgumentConverter>();
        }

        public abstract TOut Run(TIn parameter);

        Object IRunnable.Run(Object parameter)
        {
            return Run((TIn)parameter);
        }
    }
}
