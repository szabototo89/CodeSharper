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
        private readonly IRunnableManager runnableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardExecutor"/> class.
        /// </summary>
        public StandardExecutor(IRunnableManager runnableManager)
        {
            Assume.NotNull(runnableManager, nameof(runnableManager));
            this.runnableManager = runnableManager;
        }

        /// <summary>
        /// Executes the specified runnable with given parameter.
        /// </summary>
        public Object Execute(IRunnable runnable, Object parameter)
        {
            Assume.NotNull(runnable, nameof(runnable));
            var actualParameter = consume(runnable, parameter);
            return produce(runnable, actualParameter);
        }

        private Object produce(IRunnable runnable, Object result)
        {
            var producers = runnableManager.GetProducers(runnable);
            var converter = producers.FirstOrDefault(c => c.IsConvertable(result));
            if (converter == null) return result;
            return converter.Convert(result);
        }

        private Object consume(IRunnable runnable, Object parameter)
        {
            var consumers = runnableManager.GetConsumers(runnable);
            var converter = consumers.FirstOrDefault(c => c.IsConvertable(parameter));
            if (converter == null) return ExecuteRunnable(runnable, parameter);
            return converter.Convert(parameter, param => ExecuteRunnable(runnable, param));
        }

        /// <summary>
        /// Executes the runnable with specified parameter.
        /// </summary>
        protected virtual Object ExecuteRunnable(IRunnable runnable, Object parameter)
        {
            return runnable.Run(parameter);
        }
    }
}