using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Selectors.NodeSelectorOperands
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
