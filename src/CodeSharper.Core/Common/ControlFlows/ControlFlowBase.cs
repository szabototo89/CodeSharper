using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common.ControlFlows
{
    public abstract class ControlFlowBase
    {
        /// <summary>
        /// Gets or sets the executor.
        /// </summary>
        public IExecutor Executor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlFlowBase"/> class.
        /// </summary>
        protected ControlFlowBase(IExecutor executor)
        {
            Assume.NotNull(executor, "executor");
            Executor = executor;
        }

        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public abstract Object Execute(Object parameter);
    }
}