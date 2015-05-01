using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class ConcatCombinator : BinaryCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcatCombinator"/> class.
        /// </summary>
        public ConcatCombinator(CombinatorBase left, CombinatorBase right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var leftExpression = Left.Calculate(values);
            var rightExpression = Right.Calculate(values);

            return leftExpression.Concat(rightExpression);
        }
    }
}
