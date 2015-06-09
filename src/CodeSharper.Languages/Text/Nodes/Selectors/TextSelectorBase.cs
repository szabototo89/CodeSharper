using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
    public abstract class TextSelectorBase : SelectorBase
    {
        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<Object> SelectElement(Object element)
        {
            var textRange = element as TextRange;
            if (textRange == null)
                return Enumerable.Empty<Object>();

            return SelectElement(textRange);
        }

        public abstract IEnumerable<TextRange> SelectElement(TextRange textRange);
    }
}