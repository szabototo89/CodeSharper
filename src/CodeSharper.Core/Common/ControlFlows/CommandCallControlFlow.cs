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
        /// Initializes a new instance of the <see cref="CommandCallControlFlow"/> class.
        /// </summary>
        public CommandCallControlFlow(Command command, IExecutor executor)
            : base(executor)
        {
            Assume.NotNull(command, "command");
            Command = command;
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