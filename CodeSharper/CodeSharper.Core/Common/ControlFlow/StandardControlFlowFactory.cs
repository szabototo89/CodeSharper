using System;
using System.Collections.Generic;
using System.Data.Odbc;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Utilities;
using Microsoft.CSharp;

namespace CodeSharper.Core.Common.ControlFlow
{
    public class StandardControlFlowFactory : IControlFlowFactory
    {
        private readonly List<IControlFlow> _controlFlows;
        private IExecutor _executor;

        public ICommandManager CommandManager { get; protected set; }

        public StandardControlFlowFactory(ICommandManager commandManager)
        {
            Constraints.NotNull(() => commandManager);
            CommandManager = commandManager;
            _controlFlows = new List<IControlFlow>();
            _executor = new StandardExecutor(RunnableManager.Instance);
        }

        private IControlFlow _CreateControlFlow(ICommandCall commandCall)
        {
            Constraints.NotNull(() => commandCall);

            if (commandCall is PipelineCommandCall)
                return _CreatePipelineControlFlow((PipelineCommandCall)commandCall);

            throw ThrowHelper.Exception<NotSupportedException>();
        }

        private IControlFlow _CreatePipelineControlFlow(PipelineCommandCall pipelineCommandCall)
        {
            var commands = new List<ICommand>();

            foreach (var child in pipelineCommandCall.Children) {
                if (child is SingleCommandCall) {
                    var command = CommandManager.TryGetCommand(child.Cast<SingleCommandCall>().CommandCallDescriptor);
                    if (command != Option.None)
                        commands.Add(command.Value);
                }
                else {
                    ThrowHelper.ThrowException<NotSupportedException>();
                }
            }

            return new StandardControlFlow(commands);
        }

        public IControlFlow CreateControlFlow(ICommandCall commandCall)
        {
            _controlFlows.Clear();
            return _CreateControlFlow(commandCall);
        }
    }
}