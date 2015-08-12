using System;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Common
{
    public class StandardExecutorWithContext : StandardExecutor
    {
        private readonly Object context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardExecutor"/> class.
        /// </summary>
        public StandardExecutorWithContext(IRunnableManager runnableManager, Object context) : base(runnableManager)
        {
            this.context = context;
        }

        /// <summary>
        /// Executes the runnable with specified parameter.
        /// </summary>
        protected override Object ExecuteRunnable(IRunnable runnable, Object parameter)
        {
            if (runnable is IRunnableWithContext)
            {
                var runnableWithContext = (IRunnableWithContext) runnable;
                return runnableWithContext.Run(parameter, context);
            }

            return base.ExecuteRunnable(runnable, parameter);
        }
    }
}