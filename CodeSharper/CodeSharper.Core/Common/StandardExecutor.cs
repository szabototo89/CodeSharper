using System;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public class StandardExecutor<TIn, TOut>
    {
        private readonly IRunnable<TIn, TOut> _runnable;

        public StandardExecutor(IRunnable<TIn, TOut> runnable)
        {
            Constraints.NotNull(() => runnable);
            _runnable = runnable;
        }

        public Argument Execute(Argument parameter)
        {
            return UnwrapResult(WrapParameter(parameter));
        }

        private Argument UnwrapResult(Object result)
        {
            var unwrapper = _runnable.SupportedArgumentWrappers.FirstOrDefault(c => c.IsWrappable(result));

            if (unwrapper == null) return null;
            return unwrapper.Wrap(result) as Argument;
        }

        private Object WrapParameter(Argument parameter)
        {
            var converter = _runnable.SupportedArgumentUnwrappers.FirstOrDefault(c => c.IsUnwrappable(parameter));

            if (converter == null) return null;
            return converter.Unwrap(parameter, unwrapped => _runnable.Run((TIn) unwrapped));
        }
    }
}