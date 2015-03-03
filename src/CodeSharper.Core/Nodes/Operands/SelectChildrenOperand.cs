using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Operands
{
    public class SelectChildrenOperand : BinaryNodeSelectorOperand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectChildrenOperand"/> class.
        /// </summary>
        public SelectChildrenOperand(NodeSelectorOperandBase left, NodeSelectorOperandBase right) : base(left, right)
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
