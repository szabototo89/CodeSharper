using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Constants
{
    public class NumberConstant : ConstantSyntax, IHasValue<Decimal>
    {
        /// <summary>
        /// Gets or sets the value of number
        /// </summary>
        public Decimal Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberConstant"/> class.
        /// </summary>
        public NumberConstant(Decimal value, TextRange textRange) : base(textRange)
        {
            Value = value;
        }
    }
}