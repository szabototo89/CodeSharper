using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common
{
    public class StandardExecutor : IExecutor
    {
        private readonly IRunnableManager _runnableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardExecutor"/> class.
        /// </summary>
        public StandardExecutor(IRunnableManager runnableManager)
        {
            Assume.NotNull(runnableManager, "runnableManager");
            _runnableManager = runnableManager;
        }

        /// <summary>
        /// Executes the specified runnable with given parameter.
        /// </summary>
        public Object Execute(IRunnable runnable, Object parameter)
        {
            Assume.NotNull(runnable, "runnable");
            var actualParameter = consume(runnable, parameter);
            return produce(runnable, actualParameter);
        }

        private Object produce(IRunnable runnable, Object result)
        {
            var producers = _runnableManager.GetProducers(runnable);
            var converter = producers.FirstOrDefault(c => c.IsConvertable(result));
            if (converter == null) return result;
            return converter.Convert(result);
        }

        private Object consume(IRunnable runnable, Object parameter)
        {
            var consumers = _runnableManager.GetConsumers(runnable);
            var converter = consumers.FirstOrDefault(c => c.IsConvertable(parameter));
            if (converter == null) return runnable.Run(parameter);
            return converter.Convert(parameter, runnable.Run);
        }
    }
}