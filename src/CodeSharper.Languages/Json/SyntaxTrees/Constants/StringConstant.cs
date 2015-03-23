using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees
{
    public class StringConstant : ConstantSyntax, IHasValue<string>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringConstant"/> class.
        /// </summary>
        public StringConstant(String value, TextRange textRange) : base(textRange)
        {
            Assume.NotNull(value, "value");

            Value = value;
        }
    }
}