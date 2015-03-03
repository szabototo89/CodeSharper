using System;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Selectors
{
    public abstract class TypedNodeSelectorBase<TNode> : NodeSelectorBase
        where TNode : Node
    {
        public override Boolean FilterNode(Node node)
        {
            return node is TNode;
        }
    }
}
