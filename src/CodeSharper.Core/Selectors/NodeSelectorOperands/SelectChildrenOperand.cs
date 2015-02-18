using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Selectors.NodeSelectorOperands
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
