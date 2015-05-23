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
        public ISelectorResolver SelectorResolver { get; protected set; }

        /// <summary>
        /// Gets or sets the executor.
        /// </summary>
        public IExecutor Executor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultControlFlowFactory"/> class.
        /// </summary>
        public DefaultControlFlowFactory(ICommandCallResolver commandCallResolver, ISelectorResolver selectorResolver, IExecutor executor)
        {
            Assume.NotNull(commandCallResolver, "commandCallResolver");
            Assume.NotNull(selectorResolver, "SelectorResolver");
            Assume.NotNull(executor, "executor");

            CommandCallResolver = commandCallResolver;
            SelectorResolver = selectorResolver;
            Executor = executor;
        }

        #region Members for creating control flows

        private IEnumerable<ControlFlowBase> createChildren(IHasChildren<ControlFlowElementBase> controlFlow)
        {
            return controlFlow.Children.Select(Create).ToArray();
        }

        private Command getCommand(CommandCallElement commandCallElement)
        {
            var actualParameters = commandCallElement.ActualParameters.Select(Create);
            var descriptor = new CommandCallDescriptor(commandCallElement.MethodName, actualParameters);
            return CommandCallResolver.CreateCommand(descriptor);
        }

        /// <summary>
        /// Creates the specified parameter.
        /// </summary>
        private ICommandCallActualArgument Create(ActualParameterElement parameter)
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
        public ControlFlowBase Create(ControlFlowElementBase controlFlow)
        {
            if (controlFlow is SequenceControlFlowElement)
                return Create((SequenceControlFlowElement)controlFlow);

            if (controlFlow is PipelineControlFlowElement)
                return Create((PipelineControlFlowElement)controlFlow);

            if (controlFlow is CommandCallControlFlowElement)
                return Create((CommandCallControlFlowElement)controlFlow);

            if (controlFlow is SelectorControlFlowElement)
                return Create((SelectorControlFlowElement) controlFlow);

            throw new NotSupportedException("Not supported control flow descriptor!");
        }

        /// <summary>
        /// Creates the specified selectorElement.
        /// </summary>
        public ControlFlowBase Create(SelectorControlFlowElement selector)
        {
            Assume.NotNull(selector, "selectorElement");

            var combinator = SelectorResolver.Create(selector.SelectorElement);

            return new SelectorControlFlow(combinator);
        }

        /// <summary>
        /// Creates the specified sequence.
        /// </summary>
        public ControlFlowBase Create(SequenceControlFlowElement sequence)
        {
            Assume.NotNull(sequence, "sequence");
            var children = createChildren(sequence);
            return new SequenceControlFlow(children);
        }

        /// <summary>
        /// Creates the specified pipeline.
        /// </summary>
        public ControlFlowBase Create(PipelineControlFlowElement pipeline)
        {
            Assume.NotNull(pipeline, "pipeline");

            var children = createChildren(pipeline);
            return new PipelineControlFlow(children);
        }

        /// <summary>
        /// Creates the specified command call.
        /// </summary>
        public ControlFlowBase Create(CommandCallControlFlowElement commandCall)
        {
            Assume.NotNull(commandCall, "CommandCallElement");

            // resolve command by call
            var command = getCommand(commandCall.CommandCallElement);
            if (command == null) throw new Exception("Command is not available!");

            return new CommandCallControlFlow(command, Executor);
        }

        #endregion

    }
}