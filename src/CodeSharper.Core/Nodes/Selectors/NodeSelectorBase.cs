using System;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Selectors
{
    public abstract class NodeSelectorBase
    {
        /// <summary>
        /// Filters the specified node. Returns true if specified node is in the selection otherwise false.
        /// </summary>
        public abstract Boolean FilterNode(Node node);
    }
}
