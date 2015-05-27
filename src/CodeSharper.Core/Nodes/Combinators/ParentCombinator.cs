using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class ParentCombinator : BinaryCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParentCombinator"/> class.
        /// </summary>
        public ParentCombinator(CombinatorBase left, CombinatorBase right) : base(left, right)
        {
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var leftExpression = Left.Calculate(values);
            leftExpression = leftExpression
                                .OfType<IHasParent<Object>>()
                                .Select(node => node.Parent);

            return Right.Calculate(leftExpression);
        }
    }
}
