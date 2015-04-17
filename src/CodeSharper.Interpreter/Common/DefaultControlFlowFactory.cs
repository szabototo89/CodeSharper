using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ControlFlows;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class DefaultControlFlowFactory : IControlFlowFactory<ControlFlowBase>
    {
        /// <summary>
        /// Gets or sets the command resolver.
        /// </summary>
        public ICommandCallResolver CommandCallResolver { get; protected set; }

        /// <summary>
        /// Gets or sets the executor.
        /// </summary>
        public IExecutor Executor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultControlFlowFactory"/> class.
        /// </summary>
        public DefaultControlFlowFactory(ICommandCallResolver commandCallResolver, IExecutor executor)
        {
            Assume.NotNull(commandCallResolver, "CommandCallResolver");
            Assume.NotNull(executor, "executor");
            CommandCallResolver = commandCallResolver;
            Executor = executor;
        }

        #region Members for creating control flows

        private IEnumerable<ControlFlowBase> createChildren(IHasChildren<ControlFlowDescriptorBase> controlFlow)
        {
            return controlFlow.Children.Select(Create);
        }

        private Command getCommand(CommandCall commandCall)
        {
            var actualParameters = commandCall.ActualParameters.Select<ActualParameter, ICommandCallActualArgument>(parameter => {
                if (parameter.ParameterName.HasValue)
                    return new NamedCommandCallActualArgument(parameter.ParameterName, parameter.Value.Value);
                if (parameter.Position.HasValue)
                    return new PositionedCommandCallActualArgument(parameter.Position, parameter.Value.Value);

                throw new NotSupportedException("Not supported function call!");
            });

            var descriptor = new CommandCallDescriptor(commandCall.MethodName, actualParameters);
            return CommandCallResolver.CreateCommand(descriptor);
        }

        /// <summary>
        /// Creates the specified control flow.
        /// </summary>
        public ControlFlowBase Create(ControlFlowDescriptorBase controlFlow)
        {
            if (controlFlow is SequenceControlFlowDescriptor)
                return Create((SequenceControlFlowDescriptor)controlFlow);

            if (controlFlow is PipelineControlFlowDescriptor)
                return Create((PipelineControlFlowDescriptor)controlFlow);

            if (controlFlow is CommandCallControlFlowDescriptor)
                return Create((CommandCallControlFlowDescriptor)controlFlow);

            throw new NotSupportedException("Not supported control flow descriptor!");
        }

        /// <summary>
        /// Creates the specified sequence.
        /// </summary>
        public ControlFlowBase Create(SequenceControlFlowDescriptor sequence)
        {
            Assume.NotNull(sequence, "sequence");
            var children = createChildren(sequence);
            return new SequenceControlFlow(children, Executor);
        }

        /// <summary>
        /// Creates the specified pipeline.
        /// </summary>
        public ControlFlowBase Create(PipelineControlFlowDescriptor pipeline)
        {
            Assume.NotNull(pipeline, "pipeline");
            return new PipelineControlFlow(createChildren(pipeline), Executor);
        }

        /// <summary>
        /// Creates the specified command call.
        /// </summary>
        public ControlFlowBase Create(CommandCallControlFlowDescriptor commandCall)
        {
            Assume.NotNull(commandCall, "commandCall");
            var command = getCommand(commandCall.CommandCall);
            return new CommandCallControlFlow(command, Executor);
        }

        #endregion

    }
}