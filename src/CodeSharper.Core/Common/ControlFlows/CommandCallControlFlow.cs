using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common.ControlFlows
{
    public class CommandCallControlFlow : ControlFlowBase
    {
        /// <summary>
        /// Gets or sets the command of call
        /// </summary>
        public Command Command { get; protected set; }

        /// <summary>
        /// Gets or sets the executor.
        /// </summary>
        public IExecutor Executor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCallControlFlow"/> class.
        /// </summary>
        public CommandCallControlFlow(Command command, IExecutor executor)
        {
            Assume.NotNull(command, nameof(command));
            Assume.NotNull(executor, nameof(executor));
            Command = command;
            Executor = executor;
        }

        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public override Object Execute(Object parameter)
        {
            var runnable = Command.Runnable;
            return Executor.Execute(runnable, parameter);
        }
    }
}