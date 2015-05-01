﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class ChildrenNodeModifier : NodeModifierBase
    {
        /// <summary>
        /// Modifies the selection of node 
        /// </summary>
        public override IEnumerable<Object> ModifySelection(Object value)
        {
            var hasChildren = value as IHasChildren<Object>;

            if (hasChildren == null)
                return Enumerable.Empty<Object>();

            return hasChildren.Children;
        }
    }
}