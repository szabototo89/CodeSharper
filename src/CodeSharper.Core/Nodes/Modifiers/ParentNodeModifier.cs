using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class ParentNodeModifier : NodeModifierBase
    {
        /// <summary>
        /// Modifies the selection of node 
        /// </summary>
        public override IEnumerable<Node> ModifySelection(Node node)
        {
            if (node == null)
                return Enumerable.Empty<Node>();

            return new[] { node.Parent };
        }
    }
}