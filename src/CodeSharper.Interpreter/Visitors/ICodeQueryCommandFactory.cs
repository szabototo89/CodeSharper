using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        ControlFlowDescriptorBase ControlFlow(CommandCall methodCall);
    }
}
