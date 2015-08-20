using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes;
using CodeSharper.Languages.CSharp.Utilities;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static CodeSharper.Core.Utilities.ConstructsHelper;

namespace CodeSharper.Languages.CSharp.Selectors.DeclarationSelectors
{
    public class ClassDeclarationSelector : CSharpTypeDeclarationSelectorBase<ClassDeclarationSyntax>
    {
        protected readonly List<SyntaxKind> SearchedModifiers;

        private readonly String ABSTRACT_ATTRIBUTE_VALUE = "abstract";
        private readonly String SEALED_ATTRIBUTE_VALUE = "sealed";
        private readonly String PARTIAL_ATTRIBUTE_VALUE = "partial";

        public ClassDeclarationSelector()
        {
            SearchedModifiers = new List<SyntaxKind>();
        }

        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);
            ApplyClassAttributes();
        }

        private void ApplyClassAttributes()
        {
            SearchedModifiers.Clear();

            if (IsAttributeDefined(ABSTRACT_ATTRIBUTE_VALUE))
            {
                SearchedModifiers.Add(SyntaxKind.AbstractKeyword);
            }
            if (IsAttributeDefined(SEALED_ATTRIBUTE_VALUE))
            {
                SearchedModifiers.Add(SyntaxKind.SealedKeyword);
            }
            if (IsAttributeDefined(PARTIAL_ATTRIBUTE_VALUE))
            {
                SearchedModifiers.Add(SyntaxKind.PartialKeyword);
            }
        }

        public override IEnumerable<Object> SelectElement(Object element)
        {
            var selectedElements = base.SelectElement(element);
            if (!SearchedModifiers.Any()) return selectedElements;

            return SelectSpecificElements(selectedElements);
        }

        private IEnumerable<Object> SelectSpecificElements(IEnumerable<Object> selectedElements)
        {
            foreach (var selectedElement in selectedElements.OfType<ClassDeclarationSyntax>())
            {
                foreach (var modifier in SearchedModifiers)
                {
                    if (selectedElement.HasModifier(modifier))
                    {
                        yield return selectedElement;
                    }
                }
            }
        }
    }
}