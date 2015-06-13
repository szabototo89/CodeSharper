using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class ChildrenCombinator : BinaryCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChildrenCombinator"/> class.
        /// </summary>
        public ChildrenCombinator(CombinatorBase left, CombinatorBase right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var leftExpression = Left.Calculate(values);

            var children = leftExpression.OfType<IHasChildren<Object>>()
                                         .SelectMany(expression => expression.Children);

            return Right.Calculate(children);
        }
    }
}