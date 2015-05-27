using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class NodeSelectionCombinator : UnaryCombinator
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
        /// Initializes a new instance of the <see cref="NodeSelectionCombinator"/> class.
        /// </summary>
        public NodeSelectionCombinator(NodeSelectorBase nodeSelector, IEnumerable<NodeModifierBase> modifiers = null)
        {
            Assume.NotNull(nodeSelector, "nodeSelector");

            NodeSelector = nodeSelector;
            Modifiers = modifiers.GetOrEmpty();
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var filteredNodes = values.GetOrEmpty().Where(NodeSelector.FilterNode);
            foreach (var node in filteredNodes)
            {
                if (Modifiers.Any())
                {
                    foreach (var modifier in Modifiers)
                    {
                        foreach (var element in modifier.ModifySelection(node))
                        {
                            yield return element;
                        }

                        // result.AddRange(modifier.ModifySelection(node));
                    }
                }
                else
                {
                    yield return node;
                    // result.Add(node);
                }
            }
        }
    }
}