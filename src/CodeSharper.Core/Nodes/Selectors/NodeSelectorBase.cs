using System;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Selectors
{
    public abstract class NodeSelectorBase
    {
        public abstract Boolean FilterNode(Node node);
    }
}
