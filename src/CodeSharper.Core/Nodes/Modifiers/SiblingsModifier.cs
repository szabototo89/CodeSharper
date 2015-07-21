using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class SiblingsModifier : ModifierBase
    {
        /// <summary>
        /// Modifies the selection of node
        /// </summary>
        public override IEnumerable<Object> ModifySelection(Object node)
        {
            var hasParent = node as IHasParent<Object>;

            var children = hasParent.Safe(n => n.Parent as IHasChildren<Object>)
                                    .Safe(parent => parent.Children);

            if (children == null)
                return Enumerable.Empty<Object>();

            return children.Where(child => !ReferenceEquals(node, child));
        }
    }
}
