using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Interfaces;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class TextCombinator : BinaryCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryCombinator"/> class.
        /// </summary>
        public TextCombinator(CombinatorBase left, CombinatorBase right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var leftExpression = Left.Calculate(values);
            var textRanges = leftExpression.OfType<IHasTextRange>()
                                           .Select(expression => expression.TextRange);

            return Right.Calculate(textRanges);
        }
    }
}