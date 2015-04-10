using System;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Interpreter.Nodes
{
    public class ActualParameterSyntax : CodeQueryNode
    {
        /// <summary>
        /// Gets or sets the name of the parameter
        /// </summary>
        public Option<String> ParameterName { get; protected set; }

        /// <summary>
        /// Gets or sets the position of actual parameter
        /// </summary>
        public Option<Int32> Position { get; protected set; }

        /// <summary>
        /// Gets or sets the value of actual parameter
        /// </summary>
        public ConstantSyntax Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActualParameterSyntax"/> class.
        /// </summary>
        public ActualParameterSyntax(ConstantSyntax value, Int32 position)
        {
            Assume.NotNull(value, "value");

            Value = value;
            Position = Option.Some(position);
            ParameterName = Option.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActualParameterSyntax"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public ActualParameterSyntax(ConstantSyntax value, String parameterName)
        {
            Assume.NotNull(value, "value");
            Assume.NotNull(parameterName, "parameterName");

            Value = value;
            ParameterName = Option.Some(parameterName);
            Position = Option.None;
        }
    }
}