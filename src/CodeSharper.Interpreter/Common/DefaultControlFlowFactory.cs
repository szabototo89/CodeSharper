using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ControlFlows;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes;

namespace CodeSharper.Interpreter.Common
{
    public class DefaultControlFlowFactory : IControlFlowFactory<ControlFlowBase>
    {
        /// <summary>
        /// Gets or sets the command resolver.
        /// </summary>
        public ICommandCallResolver CommandCallResolver { get; protected set; }

        /// <summary>
        /// Gets or sets the node selectorElement resolver.
        /// </summary>
        public INodeSelectorResolver NodeSelectorResolver { get; protected set; }

        /// <summary>
        /// Gets or sets the executor.
        /// </summary>
        public IExecutor Executor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultControlFlowFactory"/> class.
        /// </summary>
        public DefaultControlFlowFactory(ICommandCallResolver commandCallResolver, INodeSelectorResolver nodeSelectorResolver, IExecutor executor)
        {
            Assume.NotNull(commandCallResolver, "commandCallResolver");
            Assume.NotNull(nodeSelectorResolver, "nodeSelectorResolver");
            Assume.NotNull(executor, "executor");

            CommandCallResolver = commandCallResolver;
            NodeSelectorResolver = nodeSelectorResolver;
            Executor = executor;
        }

        #region Members for creating control flows

        private IEnumerable<ControlFlowBase> createChildren(IHasChildren<ControlFlowDescriptorBase> controlFlow)
        {
            return controlFlow.Children.Select(Create).ToArray();
        }

        private Command getCommand(CommandCall commandCall)
        {
            var actualParameters = commandCall.ActualParameters.Select(Create);
            var descriptor = new CommandCallDescriptor(commandCall.MethodName, actualParameters);
            return CommandCallResolver.CreateCommand(descriptor);
        }

        /// <summary>
        /// Creates the specified parameter.
        /// </summary>
        private ICommandCallActualArgument Create(ActualParameter parameter)
        {
            if (parameter.ParameterName.HasValue)
                return new NamedCommandCallActualArgument(parameter.ParameterName, parameter.Value.Value);
            if (parameter.Position.HasValue)
                return new PositionedCommandCallActualArgument(parameter.Position, parameter.Value.Value);

            throw new NotSupportedException("Not supported function call!");
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

            if (controlFlow is SelectorControlFlowDescriptor)
                return Create((SelectorControlFlowDescriptor) controlFlow);

            throw new NotSupportedException("Not supported control flow descriptor!");
        }

        /// <summary>
        /// Creates the specified selectorElement.
        /// </summary>
        public ControlFlowBase Create(SelectorControlFlowDescriptor selector)
        {
            Assume.NotNull(selector, "selectorElement");

            var combinator = NodeSelectorResolver.Create(selector.SelectorElement);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified sequence.
        /// </summary>
        public ControlFlowBase Create(SequenceControlFlowDescriptor sequence)
        {
            Assume.NotNull(sequence, "sequence");
            var children = createChildren(sequence);
            return new SequenceControlFlow(children);
        }

        /// <summary>
        /// Creates the specified pipeline.
        /// </summary>
        public ControlFlowBase Create(PipelineControlFlowDescriptor pipeline)
        {
            Assume.NotNull(pipeline, "pipeline");

            var children = createChildren(pipeline);
            return new PipelineControlFlow(children);
        }

        /// <summary>
        /// Creates the specified command call.
        /// </summary>
        public ControlFlowBase Create(CommandCallControlFlowDescriptor commandCall)
        {
            Assume.NotNull(commandCall, "commandCall");

            // resolve command by call
            var command = getCommand(commandCall.CommandCall);
            if (command == null) throw new Exception("Command is not available!");

            return new CommandCallControlFlow(command, Executor);
        }

        #endregion

    }
}