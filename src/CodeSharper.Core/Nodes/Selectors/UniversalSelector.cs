using System;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Selectors
{
    public class UniversalSelector : NodeSelectorBase
    {
        /// <summary>
        /// Filters the specified node. Returns true if specified node is in the selection otherwise false.
        /// </summary>
        public override Boolean FilterNode(Object node)
        {
            return true;
        }
    }
}