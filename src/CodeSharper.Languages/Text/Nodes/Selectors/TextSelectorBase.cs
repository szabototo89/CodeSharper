using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Interfaces;
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
            if (textRange != null)
                return SelectElement(textRange);

            var elementWithTextRange = element as IHasTextRange;
            if (elementWithTextRange != null)
                return SelectElement(elementWithTextRange.TextRange);

            return Enumerable.Empty<Object>();
        }

        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public abstract IEnumerable<TextRange> SelectElement(TextRange textRange);
    }
}