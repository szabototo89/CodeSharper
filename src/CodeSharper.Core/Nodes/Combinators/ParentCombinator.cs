using System.Collections.Generic;
using System.Linq;
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
        /// Calculates the specified nodes.
        /// </summary>
       public override IEnumerable<Node> Calculate(IEnumerable<Node> nodes)
        {
            var leftExpression = Left.Calculate(nodes);
            leftExpression = leftExpression.Select(node => node.Parent);

            return Right.Calculate(leftExpression);
        }
    }
}
