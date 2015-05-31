using System;
using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Selectors
{
    public abstract class SelectorBase
    {
        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public abstract IEnumerable<Object> SelectElement(Object element);
    }
}
