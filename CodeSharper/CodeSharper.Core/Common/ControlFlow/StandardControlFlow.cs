using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.ControlFlow
{
    public class StandardControlFlow
    {
        private readonly List<ICommand> _commands;

        public StandardControlFlow()
        {
            _commands = new List<ICommand>();
        }

        public StandardControlFlow Clear()
        {
            _commands.Clear();
            return this;
        }

        public StandardControlFlow SetControlFlow(IEnumerable<ICommand> commands)
        {
            Constraints.NotEmpty(() => commands);
            Clear();
            _commands.AddRange(commands);
            return this;
        }

        public StandardControlFlow SetControlFlow(IEnumerable<IRunnable> runnables)
        {
            Constraints.NotEmpty(() => runnables);
            Clear();
            _commands.AddRange(runnables.Select(runnable => new ConstantCommand() { Runnable = runnable }));
            return this;
        }


        public Argument Execute(Argument parameter)
        {
            Argument result = parameter;
            var executor = Executors.StandardExecutor;

            foreach (var command in _commands)
            {
                var runnable = command.GetRunnable();
                result = executor.Execute(runnable, result);
            }

            return result;
        }
    }
}