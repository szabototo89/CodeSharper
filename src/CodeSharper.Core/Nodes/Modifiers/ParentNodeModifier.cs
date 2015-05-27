using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class ParentNodeModifier : NodeModifierBase
    {
        /// <summary>
        /// Modifies the selection of node 
        /// </summary>
        public override IEnumerable<Object> ModifySelection(Object node)
        {
            var hasParent = node as IHasParent<Object>;
            if (hasParent == null)
                yield break;

            yield return hasParent.Parent;
        }
    }
}