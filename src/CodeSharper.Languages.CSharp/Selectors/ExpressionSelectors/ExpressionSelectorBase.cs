using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes.Selectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeSharper.Languages.CSharp.Selectors.ExpressionSelectors
{
    public abstract class ExpressionSelectorBase<TExpressionType> : TypedSelectorBase<TExpressionType>
    {
        private readonly SyntaxKind? kind;

        protected ExpressionSelectorBase(SyntaxKind? kind = null)
        {
            this.kind = kind;
        }

        public override IEnumerable<Object> SelectElement(Object element)
        {
            if (kind == null) return base.SelectElement(element);
            return SelectElementBySyntaxKind(element, kind.Value);
        }

        private IEnumerable<Object> SelectElementBySyntaxKind(Object element, SyntaxKind syntaxKind)
        {
            var selectedElements = base.SelectElement(element).OfType<SyntaxNode>();

            foreach (var selectedElement in selectedElements)
            {
                if (selectedElement.Kind() == syntaxKind)
                {
                    yield return selectedElement;
                }
            }
        }
    }
}