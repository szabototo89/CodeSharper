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
        Constant CreateBoolean(Boolean value);

        /// <summary>
        /// Creates a number literal value
        /// </summary>
        Constant CreateNumber(Double value);

        /// <summary>
        /// Creates a string literal value
        /// </summary>
        Constant CreateString(String value);

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        ActualParameter CreateActualParameter(Constant value, String parameterName);

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        ActualParameter CreateActualParameter(Constant value, Int32 position);

        /// <summary>
        /// Creates a method call symbol node
        /// </summary>
        CommandCall CreateMethodCall(String name, IEnumerable<ActualParameter> parameters);

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        ControlFlowDescriptorBase CreateControlFlow(String @operator, CommandCall commandCall, ControlFlowDescriptorBase rightExpression);

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        ControlFlowDescriptorBase CreateControlFlow(CommandCall methodCall);

        /// <summary>
        /// Creates a control flow for selection.
        /// </summary>
        ControlFlowDescriptorBase CreateControlFlow(BaseSelector selector);
    }
}
