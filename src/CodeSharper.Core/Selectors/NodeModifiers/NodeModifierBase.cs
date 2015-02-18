using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Selectors.NodeModifiers
{
    public abstract class NodeModifierBase
    {
        /// <summary>
        /// Modifies the selection of node 
        /// </summary>
        public abstract IEnumerable<Node> ModifySelection(Node node);
    }
}
