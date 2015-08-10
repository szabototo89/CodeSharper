using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Constants
{
    public class StringConstant : ConstantSyntax, IHasValue<String>
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
            Assume.NotNull(value, nameof(value));

            Value = value;
        }
    }
}