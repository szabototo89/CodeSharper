using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.ControlFlow
{
    public class StandardControlFlow : IControlFlow
    {
        private readonly List<ICommand> _commands;
        private readonly IExecutor _executor;

        public ICommandManager CommandManager { get; protected set; }

        public StandardControlFlow(IEnumerable<ICommand> commands)
        {
            Constraints.NotNull(() => commands);

            _commands = commands.ToList();
        }

        public StandardControlFlow(ICommandManager commandManager, IExecutor executor)
        {
            Constraints.NotNull(() => commandManager);
            Constraints.NotNull(() => executor);

            CommandManager = commandManager;
            _executor = executor;

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
            _commands.AddRange(runnables.Select(runnable => new Command(runnable, CommandDescriptor.Empty, new CommandArgumentCollection())));
            return this;
        }


        public Argument Execute(Argument parameter)
        {
            Argument result = parameter;

            foreach (var command in _commands) {
                var runnable = command.Runnable;
                result = _executor.Execute(runnable, result);
            }

            return result;
        }

        public StandardControlFlow ParseCommandCall(ICommandCall commandCall)
        {
            _commands.Clear();
            return parseCommandCallWithOutInitialization(commandCall);
        }

        private StandardControlFlow parseCommandCallWithOutInitialization(ICommandCall commandCall)
        {
            Constraints.NotNull(() => commandCall);

            if (commandCall is SingleCommandCall) {
                var command = CommandManager.TryGetCommand((commandCall as SingleCommandCall).CommandCallDescriptor);
                if (command != Option.None)
                    _commands.Add(command.Value);
            }
            else if (commandCall is PipelineCommandCall) {
                var call = commandCall as PipelineCommandCall;
                foreach (var child in call.Children)
                    parseCommandCallWithOutInitialization(child);
            }

            return this;
        }
    }
}