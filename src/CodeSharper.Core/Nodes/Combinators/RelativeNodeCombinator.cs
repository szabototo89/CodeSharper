using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class RelativeNodeCombinator : BinaryCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeNodeCombinator"/> class.
        /// </summary>
        public RelativeNodeCombinator(CombinatorBase left, CombinatorBase right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var result = new List<Object>();
            var leftExpression = Left.Calculate(values).OfType<IHasChildren<Object>>();
            foreach (var node in leftExpression)
            {
                var children = node.ToEnumerable();
                result.AddRange(Right.Calculate(children));
            }
            return result;
        }
    }
}