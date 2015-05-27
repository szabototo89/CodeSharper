using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Constants
{
    public class BooleanConstant : ConstantSyntax, IHasValue<Boolean>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BooleanConstant"/> is value.
        /// </summary>
        public Boolean Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanConstant"/> class.
        /// </summary>
        public BooleanConstant(Boolean value, TextRange textRange) : base(textRange)
        {
            Value = value;
        }
    }
}