using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.CSharp.Selectors.ClassSelectors;
using CodeSharper.Languages.CSharp.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Selectors
{
    public class CSharpTypeDeclarationSelectorBase<TElement> : CSharpTypedSelectorBase<TElement, IdentifierMatchingClassSelector>
        where TElement : BaseTypeDeclarationSyntax
    {
        protected readonly Dictionary<String, SyntaxKind> VisibilityDeclarations = new Dictionary<String, SyntaxKind>
        {
            ["public"] = SyntaxKind.PublicKeyword,
            ["private"] = SyntaxKind.PrivateKeyword,
            ["protected"] = SyntaxKind.ProtectedKeyword,
            ["internal"] = SyntaxKind.InternalKeyword
        };

        private readonly String VISIBILITY_ATTRIBUTE_NAME = "visibility";

        protected IEnumerable<SyntaxKind> Visibilities { get; private set; }

        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            var visibilities = new List<SyntaxKind>();

            foreach (var visibility in VisibilityDeclarations)
            {
                var isVisibilityDeclaredAsModifierAttribute = GetAttributeValueOrDefault(VISIBILITY_ATTRIBUTE_NAME, defaultValue: String.Empty) == visibility.Key;
                if (isVisibilityDeclaredAsModifierAttribute || IsAttributeDefined(visibility.Key))
                {
                    visibilities.Add(visibility.Value);
                }
            }

            Visibilities = visibilities.AsReadOnly();
        }

        public override IEnumerable<Object> SelectElement(Object element)
        {
            var selectedElements = base.SelectElement(element);
            if (!Visibilities.GetOrEmpty().Any()) return selectedElements;

            return SelectElementsByVisibility(selectedElements);
        }

        private IEnumerable<Object> SelectElementsByVisibility(IEnumerable<Object> selectedElements)
        {
            foreach (var element in selectedElements.OfType<TypeDeclarationSyntax>())
            {
                if (Visibilities.Any(visibility => element.HasModifier(visibility)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Selects the syntax token.
        /// </summary>
        protected override SyntaxToken SelectSyntaxToken(TElement element)
        {
            return element.Identifier;
        }
    }
}