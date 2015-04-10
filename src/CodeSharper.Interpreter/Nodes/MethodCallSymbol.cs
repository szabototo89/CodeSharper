using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Nodes
{
    public class MethodCallSymbol : CodeQueryNode
    {
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public String MethodName { get; protected set; }

        /// <summary>
        /// Gets or sets the actual parameters.
        /// </summary>
        public IEnumerable<ActualParameterSyntax> ActualParameters { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCallSymbol"/> class.
        /// </summary>
        public MethodCallSymbol(String methodName, IEnumerable<ActualParameterSyntax> actualParameters)
        {
            Assume.NotNull(methodName, "methodName");
            Assume.NotNull(actualParameters, "actualParameters");

            MethodName = methodName;
            ActualParameters = actualParameters;
        }
    }

    public class ControlFlowSymbol : CodeQueryNode
    {
        
    }
}