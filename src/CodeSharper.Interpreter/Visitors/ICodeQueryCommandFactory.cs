using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Common;

namespace CodeSharper.Interpreter.Visitors
{
    public interface ICodeQueryCommandFactory
    {
        /// <summary>
        /// Creates a boolean literal value
        /// </summary>
        Constant Boolean(Boolean value);

        /// <summary>
        /// Creates a number literal value
        /// </summary>
        Constant Number(Double value);

        /// <summary>
        /// Creates a string literal value
        /// </summary>
        Constant String(String value);

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        ActualParameter ActualParameter(Constant value, String parameterName);

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        ActualParameter ActualParameter(Constant value, Int32 position);

        /// <summary>
        /// Creates a method call symbol node
        /// </summary>
        CommandCall MethodCall(String name, IEnumerable<ActualParameter> parameters);

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        ControlFlowDescriptorBase ControlFlow(String @operator, CommandCall commandCall, ControlFlowDescriptorBase rightExpression);
    }

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
            var controlFlowChildren = new[] { rightExpression };

            switch (@operator)
            {
                case PIPELINE_OPERATOR:
                    return new PipelineControlFlowDescriptor(controlFlowChildren);
                case SEQUENCE_OPERATOR:
                    return new SequenceControlFlowDescriptor(controlFlowChildren);
                case LAZY_AND_OPERATOR:
                    return new LazyAndControlFlowDescriptor(controlFlowChildren);
            }

            return new CommandCallControlFlowDescriptor(commandCall);
        }
    }
}
