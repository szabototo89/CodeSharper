using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Selectors.NodeModifiers;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Selectors.NodeSelectorOperands
{
    public class SelectRelativeNodesOperand : SelectAbsoluteNodesOperand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectRelativeNodesOperand"/> class.
        /// </summary>
        public SelectRelativeNodesOperand(NodeSelectorBase nodeSelector, IEnumerable<NodeModifierBase> nodeModifiers = null) : base(nodeSelector, nodeModifiers)
        {

        }

        public override IEnumerable<Node> Calculate(IEnumerable<Node> nodes)
        {
            var relativeNodes = new List<Node>();

            foreach (var node in nodes)
            {
                var children = node.ToEnumerable();
                relativeNodes.AddRange(children);
            }

            var result = base.Calculate(relativeNodes);
            return result;
        }
    }
}