using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Selectors.NodeModifiers
{
    public class SiblingsNodeModifier : NodeModifierBase
    {
        /// <summary>
        /// Modifies the selection of node
        /// </summary>
        public override IEnumerable<Node> ModifySelection(Node node)
        {
            var children = node.Safe(n => n.Parent)
                               .Safe(parent => parent.Children);

            if (children == null)
                return Enumerable.Empty<Node>();

            return children.Where(child => !ReferenceEquals(node, child));
        }
    }
}
