using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Selectors.NodeModifiers;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Selectors.NodeSelectorOperands
{
    public class SelectAbsoluteNodesOperand : NodeSelectorOperandBase
    {
        /// <summary>
        /// Gets or sets the node selector
        /// </summary>
        public NodeSelectorBase NodeSelector { get; protected set; }

        /// <summary>
        /// Gets the node modifiers.
        /// </summary>
        public IEnumerable<NodeModifierBase> NodeModifiers { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectAbsoluteNodesOperand"/> class.
        /// </summary>
        public SelectAbsoluteNodesOperand(NodeSelectorBase nodeSelector, IEnumerable<NodeModifierBase> nodeModifiers = null)
        {
            Assume.NotNull(nodeSelector, "nodeSelector");

            NodeSelector = nodeSelector;
            NodeModifiers = nodeModifiers ?? Enumerable.Empty<NodeModifierBase>();
        }

        public override IEnumerable<Node> Calculate(IEnumerable<Node> nodes)
        {
            var filteredNodes = nodes.Where(node => NodeSelector.FilterNode(node));

            var results = new List<Node>();

            if (NodeModifiers.Any())
            {
                foreach (var node in filteredNodes)
                {
                    foreach (var modifier in NodeModifiers)
                    {
                        var modifiedNodes = modifier.ModifySelection(node);
                        results.AddRange(modifiedNodes);
                    }
                }
            }
            else
            {
                results.AddRange(filteredNodes);
            }

            return results.AsReadOnly();
        }
    }
}
