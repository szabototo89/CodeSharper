using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Nodes;

namespace CodeSharper.Interpreter.Visitors
{
    public interface ICodeQuerySyntaxTreeFactory
    {
        /// <summary>
        /// Creates a boolean literal value
        /// </summary>
        ConstantSyntax Boolean(Boolean value);

        /// <summary>
        /// Creates a number literal value
        /// </summary>
        ConstantSyntax Number(Double value);

        /// <summary>
        /// Creates a string literal value
        /// </summary>
        ConstantSyntax String(String value);

        /// <summary>
        /// Creates a method call actual parameter node
        /// </summary>
        ActualParameterSyntax ActualParameter(ConstantSyntax value);

        /// <summary>
        /// Creates a method call symbol node
        /// </summary>
        MethodCallSymbol MethodCall(String name, IEnumerable<ActualParameterSyntax> parameters);

        /// <summary>
        /// Creates a control flow symbol node
        /// </summary>
        ControlFlowSymbol ControlFlow(String @operator, MethodCallSymbol methodCall, ControlFlowSymbol rightExpression);
    }
}
