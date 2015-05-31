using System;
using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Selectors
{
    public abstract class TypedSelectorBase<TNode> : SelectorBase
        where TNode : Node
    {
        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<Object> SelectElement(Object element)
        {
            if (element is TNode)
            {
                yield return element;
            }
        }
    }
}
