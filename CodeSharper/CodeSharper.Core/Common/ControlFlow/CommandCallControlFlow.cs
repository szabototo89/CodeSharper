using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Commands.CommandFactories;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.ControlFlow
{
    public class CommandCallControlFlow : IControlFlow
    {
        public IExecutor Executor { get; private set; }
        public ICommand Command { get; private set; }

        public CommandCallControlFlow(ICommand command, IExecutor executor)
        {
            Constraints.NotNull(() => executor);
            Constraints.NotNull(() => command);

            Executor = executor;
            Command = command;
        }

        public CommandCallControlFlow(ICommand command)
            : this(command, null)
        {
            
        }

        public Argument Execute(Argument parameter)
        {
            return Executor.Execute(Command.Runnable, parameter);
        }
    }
}
