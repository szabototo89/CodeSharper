using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Common;

namespace CodeSharper.Interpreter.Visitors
{
    public class DefaultCodeQueryCommandFactory : ICodeQueryCommandFactory
    {
        private const String LAZY_AND_OPERATOR = "&&";
        private const String SEQUENCE_OPERATOR = ";";
        private const String PIPELINE_OPERATOR = "|";

        /// <summary>
        /// Creates a boolean literal value
        /// </summary>
        public Constant CreateBoolean(Boolean value)
        {
            return new Constant(value, typeof(Boolean));
        }

        /// <summary>
        /// Creates a number literal value
        /// </summary>
        public Constant CreateNumber(Double value)
        {
            return new Constant(value, typeof(Double));
        }

        /// <summary>
        /// Creates a string literal value
        /// </summary>
        public Constant CreateString(String value)
        {
            Assume.NotNull(value, "value");
            return new Constant(value, typeof(String));
        }

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        public ActualParameter CreateActualParameter(Constant value, String parameterName)
        {
            return new ActualParameter(value, parameterName);
        }

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        public ActualParameter CreateActualParameter(Constant value, Int32 position)
        {
            return new ActualParameter(value, position);
        }

        /// <summary>
        /// Creates a method call symbol node
        /// </summary>
        public CommandCall CreateMethodCall(String name, IEnumerable<ActualParameter> parameters)
        {
            return new CommandCall(name, parameters);
        }

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        public ControlFlowDescriptorBase CreateControlFlow(ControlFlowDescriptorBase left, ControlFlowDescriptorBase right, String @operator)
        {
            Assume.NotNull(left, "left");
            Assume.NotNull(right, "right");
            Assume.NotNull(@operator, "operator");

            var children = new[] { left, right };

            switch (@operator)
            {
                case PIPELINE_OPERATOR:
                    return new PipelineControlFlowDescriptor(children);
                case SEQUENCE_OPERATOR:
                    return new SequenceControlFlowDescriptor(children);
                case LAZY_AND_OPERATOR:
                    return new LazyAndControlFlowDescriptor(children);
                default:
                    throw new NotSupportedException(String.Format("Not supported operator: {0}", @operator));
            }
        }

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        public ControlFlowDescriptorBase CreateControlFlow(CommandCall methodCall)
        {
            Assume.NotNull(methodCall, "selector");
            return new CommandCallControlFlowDescriptor(methodCall);
        }

        /// <summary>
        /// Creates a control flow for selection.
        /// </summary>
        public ControlFlowDescriptorBase CreateControlFlow(BaseSelector selector)
        {
            Assume.NotNull(selector, "selector");
            return new SelectorControlFlowDescriptor(selector);
        }
    }
}