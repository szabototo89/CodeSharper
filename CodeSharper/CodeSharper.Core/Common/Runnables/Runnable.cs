using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    // TODO: Refactor this section (Consumes/Produces) to another static class
    public abstract class Runnable<TIn, TOut> : IRunnable<TIn, TOut>
    {
        private readonly List<IArgumentWrapper> _supportedArgumentWrappers;
        private readonly List<IArgumentUnwrapper> _supportedArgumentUnwrappers;

        public IEnumerable<IArgumentWrapper> SupportedArgumentWrappers { get { return _supportedArgumentWrappers.AsReadOnly(); } }

        public IEnumerable<IArgumentUnwrapper> SupportedArgumentUnwrappers { get { return _supportedArgumentUnwrappers.AsReadOnly(); } }

        protected void Consumes<TArgumentUnwrapper>()
            where TArgumentUnwrapper : IArgumentUnwrapper, new()
        {
            _supportedArgumentUnwrappers.Add(new TArgumentUnwrapper());
        }

        protected void Produces<TArgumentWrapper>()
            where TArgumentWrapper : IArgumentWrapper, new()
        {
            _supportedArgumentWrappers.Add(new TArgumentWrapper());
        }
        protected Runnable()
        {
            _supportedArgumentWrappers = new List<IArgumentWrapper>();
            _supportedArgumentUnwrappers = new List<IArgumentUnwrapper>();
        }

        public abstract TOut Run(TIn parameter);

        Object IRunnable.Run(Object parameter)
        {
            return Run((TIn)parameter);
        }
    }
}