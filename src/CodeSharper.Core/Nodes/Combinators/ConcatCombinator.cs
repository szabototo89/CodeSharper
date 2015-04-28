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
        public ConcatCombinator(CombinatorBase left, CombinatorBase right) : base(left, right)
        {
        }

        public override IEnumerable<Node> Calculate(IEnumerable<Node> nodes)
        {
            var leftExpression = Left.Calculate(nodes);
            var rightExpression = Right.Calculate(nodes);

            return leftExpression.Concat(rightExpression);
        }
    }
}
