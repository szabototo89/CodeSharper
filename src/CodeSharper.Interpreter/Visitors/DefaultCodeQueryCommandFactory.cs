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
        public ConstantElement CreateBoolean(Boolean value)
        {
            return new ConstantElement(value, typeof(Boolean));
        }

        /// <summary>
        /// Creates a number literal value
        /// </summary>
        public ConstantElement CreateNumber(Double value)
        {
            return new ConstantElement(value, typeof(Double));
        }

        /// <summary>
        /// Creates a string literal value
        /// </summary>
        public ConstantElement CreateString(String value)
        {
            Assume.NotNull(value, "value");
            return new ConstantElement(value, typeof(String));
        }

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        public ActualParameterElement CreateActualParameter(ConstantElement value, String parameterName)
        {
            return new ActualParameterElement(value, parameterName);
        }

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        public ActualParameterElement CreateActualParameter(ConstantElement value, Int32 position)
        {
            return new ActualParameterElement(value, position);
        }

        /// <summary>
        /// Creates a method call symbol node
        /// </summary>
        public CommandCallElement CreateMethodCall(String name, IEnumerable<ActualParameterElement> parameters)
        {
            return new CommandCallElement(name, parameters);
        }

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        public ControlFlowElementBase CreateControlFlow(ControlFlowElementBase left, ControlFlowElementBase right, String @operator)
        {
            Assume.NotNull(left, "left");
            Assume.NotNull(right, "right");
            Assume.NotNull(@operator, "operator");

            var children = new[] { left, right };

            switch (@operator)
            {
                case PIPELINE_OPERATOR:
                    return new PipelineControlFlowElement(children);
                case SEQUENCE_OPERATOR:
                    return new SequenceControlFlowElement(children);
                case LAZY_AND_OPERATOR:
                    return new LazyAndControlFlowElement(children);
                default:
                    throw new NotSupportedException(String.Format("Not supported operator: {0}", @operator));
            }
        }

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        public ControlFlowElementBase CreateControlFlow(CommandCallElement methodCallElement)
        {
            Assume.NotNull(methodCallElement, "selectorElement");
            return new CommandCallControlFlowElement(methodCallElement);
        }

        /// <summary>
        /// Creates a control flow for selection.
        /// </summary>
        public ControlFlowElementBase CreateControlFlow(SelectorElementBase selectorElement)
        {
            Assume.NotNull(selectorElement, "selectorElement");
            return new SelectorControlFlowElement(selectorElement);
        }
    }
}