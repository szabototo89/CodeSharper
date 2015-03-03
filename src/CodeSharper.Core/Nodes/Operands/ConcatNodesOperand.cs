using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Operands
{
    public class ConcatNodesOperand : BinaryNodeSelectorOperand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcatNodesOperand"/> class.
        /// </summary>
        public ConcatNodesOperand(NodeSelectorOperandBase left, NodeSelectorOperandBase right) : base(left, right)
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
