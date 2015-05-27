using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Combinators
{
    // TODO: Remove this class
    [Obsolete("Use RelativeNodeCombinator instead of this")]
    public class RelativeCombinator : AbsoluteCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeCombinator"/> class.
        /// </summary>
        public RelativeCombinator(NodeSelectorBase nodeSelector, IEnumerable<NodeModifierBase> nodeModifiers = null)
            : base(nodeSelector, nodeModifiers)
        {

        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var relativeNodes = new List<Object>();

            foreach (var node in values.OfType<IHasChildren<Object>>())
            {
                var children = node.ToEnumerable();
                relativeNodes.AddRange(children);
            }

            var result = base.Calculate(relativeNodes);
            return result;
        }
    }
}