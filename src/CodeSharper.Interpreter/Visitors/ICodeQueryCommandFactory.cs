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
        ConstantElement CreateBoolean(Boolean value);

        /// <summary>
        /// Creates a number literal value
        /// </summary>
        ConstantElement CreateNumber(Double value);

        /// <summary>
        /// Creates a string literal value
        /// </summary>
        ConstantElement CreateString(String value);

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        ActualParameterElement CreateActualParameter(ConstantElement value, String parameterName);

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        ActualParameterElement CreateActualParameter(ConstantElement value, Int32 position);

        /// <summary>
        /// Creates a method call symbol node
        /// </summary>
        CommandCall CreateMethodCall(String name, IEnumerable<ActualParameterElement> parameters);

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        ControlFlowElementBase CreateControlFlow(ControlFlowElementBase left, ControlFlowElementBase right, String @operator);

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        ControlFlowElementBase CreateControlFlow(CommandCall methodCall);

        /// <summary>
        /// Creates a control flow for selection.
        /// </summary>
        ControlFlowElementBase CreateControlFlow(SelectorElementBase selectorElement);
    }
}
