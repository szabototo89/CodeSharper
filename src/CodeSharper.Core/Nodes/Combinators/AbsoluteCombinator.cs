using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Combinators
{
    [Obsolete("Use ChildrenCombinator instead of this")]
    public class AbsoluteCombinator : CombinatorBase
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
        /// Initializes a new instance of the <see cref="AbsoluteCombinator"/> class.
        /// </summary>
        public AbsoluteCombinator(NodeSelectorBase nodeSelector, IEnumerable<NodeModifierBase> nodeModifiers = null)
        {
            Assume.NotNull(nodeSelector, "nodeSelector");

            NodeSelector = nodeSelector;
            NodeModifiers = nodeModifiers.GetOrEmpty();
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var filteredNodes = values.Where(node => NodeSelector.FilterNode(node));

            if (NodeModifiers.Any())
            {
                foreach (var node in filteredNodes)
                {
                    foreach (var modifier in NodeModifiers)
                    {
                        var modifiedNodes = modifier.ModifySelection(node);
                        foreach (var modifiedNode in modifiedNodes)
                        {
                            yield return modifiedNode;
                        }
                    }
                }
            }
            else
            {
                foreach (var filteredNode in filteredNodes)
                {
                    yield return filteredNode;
                }       
            }
        }
    }
}
