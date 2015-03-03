using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Operands
{
    public class SelectParentNodeOperand : BinaryNodeSelectorOperand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectParentNodeOperand"/> class.
        /// </summary>
        public SelectParentNodeOperand(NodeSelectorOperandBase left, NodeSelectorOperandBase right) : base(left, right)
        {
        }

        public override IEnumerable<Node> Calculate(IEnumerable<Node> nodes)
        {
            var leftExpression = Left.Calculate(nodes);
            leftExpression = leftExpression.Select(node => node.Parent);

            return Right.Calculate(leftExpression);
        }
    }
}
