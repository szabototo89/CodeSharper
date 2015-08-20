using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Utilities;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Selectors.DeclarationSelectors
{
    public class ClassDeclarationSelector : CSharpTypeDeclarationSelectorBase<ClassDeclarationSyntax>
    {
        public override IEnumerable<Object> SelectElement(Object element)
        {
            var selectedElements = base.SelectElement(element);
            if (!IsAttributeDefined()) return selectedElements;

            return SelectSpecificElements(selectedElements);
        }

        private IEnumerable<Object> SelectSpecificElements(IEnumerable<Object> selectedElements)
        {
            foreach (var selectedElement in selectedElements.OfType<ClassDeclarationSyntax>())
            {
                if (IsAttributeDefined("abstract"))
                {
                    if (IsAbstractClass(selectedElement))
                    {
                        yield return selectedElement;
                    }
                }
            }
        }

        private static Boolean IsAbstractClass(BaseTypeDeclarationSyntax selectedElement)
        {
            return selectedElement.Modifiers.Any(modifier => modifier.Kind() == SyntaxKind.AbstractKeyword);
        }
    }
}