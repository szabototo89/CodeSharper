﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Selectors
{
    public abstract class TypedSelectorBase<TElement> : SelectorBase
    {
        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<Object> SelectElement(Object element)
        {
            if (element is TElement)
            {
                return new[] { element };
            }

            return Enumerable.Empty<Object>();
        }
    }
}