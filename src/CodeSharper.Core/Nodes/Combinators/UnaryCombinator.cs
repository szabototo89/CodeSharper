using System.Collections.Generic;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;
using Microsoft.Win32.SafeHandles;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class UnaryCombinator : CombinatorBase
    {
        /// <summary>
        /// Gets or sets the node selector.
        /// </summary>
        public NodeSelectorBase NodeSelector { get; protected set; }

        /// <summary>
        /// Gets or sets the modifiers.
        /// </summary>
        public IEnumerable<NodeModifierBase> Modifiers { get; protected set; }

        /// <summary>
        /// Calculates the specified nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        public override IEnumerable<Node> Calculate(IEnumerable<Node> nodes)
        {
            var result = new List<Node>();

            foreach (var node in nodes)
            {
                if (NodeSelector.FilterNode(node))
                {
                    foreach (var modifier in Modifiers)
                    {
                        result.AddRange(modifier.ModifySelection(node));
                    }
                }
            }

            return result;
        }
    }
}