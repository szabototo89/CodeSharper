using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class ChildrenCombinator : BinaryCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChildrenCombinator"/> class.
        /// </summary>
        public ChildrenCombinator(CombinatorBase left, CombinatorBase right) : base(left, right)
        {
        }

        public override IEnumerable<Node> Calculate(IEnumerable<Node> nodes)
        {
            var leftExpression = Left.Calculate(nodes);
            leftExpression = leftExpression.SelectMany(node => node.Children);

            return Right.Calculate(leftExpression);
        }
    }
}
