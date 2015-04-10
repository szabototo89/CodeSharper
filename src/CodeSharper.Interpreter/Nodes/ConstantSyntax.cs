using System;
using CodeSharper.Core.Common;

namespace CodeSharper.Interpreter.Nodes
{
    public class ConstantSyntax : CodeQueryNode, IHasValue<Object>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public Object Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantSyntax"/> class.
        /// </summary>
        public ConstantSyntax(Object value)
        {
            Value = value;
        }
    }
}