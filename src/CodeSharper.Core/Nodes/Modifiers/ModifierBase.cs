using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public abstract class ModifierBase
    {
        /// <summary>
        /// Modifies the selection of node 
        /// </summary>
        public abstract IEnumerable<Object> ModifySelection(Object value);
    }
}
