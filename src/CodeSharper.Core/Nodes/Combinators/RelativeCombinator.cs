using System.Collections.Generic;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class RelativeCombinator : AbsoluteCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeCombinator"/> class.
        /// </summary>
        public RelativeCombinator(NodeSelectorBase nodeSelector, IEnumerable<NodeModifierBase> nodeModifiers = null) : base(nodeSelector, nodeModifiers)
        {

        }

        /// <summary>
        /// Calculates the specified nodes.
        /// </summary>
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