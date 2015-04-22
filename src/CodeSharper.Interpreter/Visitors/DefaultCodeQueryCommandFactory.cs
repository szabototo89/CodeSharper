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
        public Constant Boolean(Boolean value)
        {
            return new Constant(value, typeof(Boolean));
        }

        /// <summary>
        /// Creates a number literal value
        /// </summary>
        public Constant Number(Double value)
        {
            return new Constant(value, typeof(Double));
        }

        /// <summary>
        /// Creates a string literal value
        /// </summary>
        public Constant String(String value)
        {
            Assume.NotNull(value, "value");
            return new Constant(value, typeof(String));
        }

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        public ActualParameter ActualParameter(Constant value, String parameterName)
        {
            return new ActualParameter(value, parameterName);
        }

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        public ActualParameter ActualParameter(Constant value, Int32 position)
        {
            return new ActualParameter(value, position);
        }

        /// <summary>
        /// Creates a method call symbol node
        /// </summary>
        public CommandCall MethodCall(String name, IEnumerable<ActualParameter> parameters)
        {
            return new CommandCall(name, parameters);
        }

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        public ControlFlowDescriptorBase ControlFlow(String @operator, CommandCall commandCall, ControlFlowDescriptorBase rightExpression)
        {
            var controlFlowChildren = new[] { ControlFlow(commandCall), rightExpression };

            switch (@operator)
            {
                case PIPELINE_OPERATOR:
                    return new PipelineControlFlowDescriptor(controlFlowChildren);
                case SEQUENCE_OPERATOR:
                    return new SequenceControlFlowDescriptor(controlFlowChildren);
                case LAZY_AND_OPERATOR:
                    return new LazyAndControlFlowDescriptor(controlFlowChildren);
                default:
                    throw new NotSupportedException(System.String.Format("Not supported operator: {0}", @operator));
            }
        }

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        public ControlFlowDescriptorBase ControlFlow(CommandCall methodCall)
        {
            Assume.NotNull(methodCall, "methodCall");
            return new CommandCallControlFlowDescriptor(methodCall);
        }

        /// <summary>
        /// Creates a selector element
        /// </summary>
        public SelectorElementAttribute SelectorElement(String name, Constant value)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(value, "value");

            return new SelectorElementAttribute {
                Name = name,
                Value = value
            };
        }

        /// <summary>
        /// Creates a pseudo selector
        /// </summary>
        public PseudoSelector PseudoSelector(String name, Constant value)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(value, "value");

            return new PseudoSelector {
                Name = name,
                Value = value
            };
        }

        /// <summary>
        /// Creates a selectable element
        /// </summary>
        public SelectableElement SelectableElement(String name, IEnumerable<SelectorElementAttribute> attributes, IEnumerable<PseudoSelector> pseudoSelectors)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(attributes, "attributes");
            Assume.NotNull(pseudoSelectors, "pseudoSelectors");

            return new SelectableElement(name, attributes, pseudoSelectors);
        }
    }
}