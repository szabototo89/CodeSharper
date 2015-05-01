using System;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Selectors
{
    public abstract class TypedNodeSelectorBase<TNode> : NodeSelectorBase
        where TNode : Node
    {
        /// <summary>
        /// Filters the specified node. Returns true if specified node is in the selection otherwise false.
        /// </summary>
        public override Boolean FilterNode(Object node)
        {
            return node is TNode;
        }
    }
}
