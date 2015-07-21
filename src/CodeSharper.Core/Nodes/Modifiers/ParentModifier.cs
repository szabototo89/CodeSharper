using System;
using System.Collections.Generic;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class ParentModifier : ModifierBase
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