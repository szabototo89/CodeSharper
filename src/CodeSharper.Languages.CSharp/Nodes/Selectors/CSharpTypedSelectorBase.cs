using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Languages.CSharp.Nodes.Selectors.ClassSelectors;
using Microsoft.CodeAnalysis;

namespace CodeSharper.Languages.CSharp.Nodes.Selectors
{
    public abstract class CSharpTypedSelectorBase<TElement, TClassSelector> : TypedSelectorBase<TElement>
        where TElement : SyntaxNode
        where TClassSelector : IClassSelector, new()
    {
        private readonly TClassSelector classSelector;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSharpTypedSelectorBase{TElement, TClassSelector}"/> class.
        /// </summary>
        protected CSharpTypedSelectorBase()
        {
            classSelector = new TClassSelector();
        }

        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<Object> SelectElement(Object element)
        {
            if (!(element is TElement))
                yield break;

            if (!ClassSelectors.Any())
                yield return element;

            var token = SelectSyntaxToken((TElement) element);
            if (classSelector.Filter(ClassSelectors, token)) yield return element;
        }

        /// <summary>
        /// Selects the syntax token.
        /// </summary>
        protected abstract SyntaxToken SelectSyntaxToken(TElement element);
    }
}