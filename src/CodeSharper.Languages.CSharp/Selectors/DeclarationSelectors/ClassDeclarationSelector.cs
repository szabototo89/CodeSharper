using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.CSharp.Utilities;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Selectors.DeclarationSelectors
{
    public class ClassDeclarationSelector : CSharpTypeDeclarationSelectorBase<ClassDeclarationSyntax>
    {
        protected Boolean IsAbstractClassDefined { get; private set; }

        protected Boolean IsSealedClassDefined { get; private set; }

        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            IsAbstractClassDefined = IsAttributeDefined("abstract");
            IsSealedClassDefined = IsAttributeDefined("sealed");
        }

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
                if (IsAbstractClassDefined)
                {
                    if (IsAbstractClass(selectedElement))
                    {
                        yield return selectedElement;
                    }
                }
                else if (IsSealedClassDefined)
                {
                    if (IsSealedClass(selectedElement))
                    {
                        yield return selectedElement;
                    }
                }
                else
                {
                    yield return selectedElement;
                }
            }
        }

        private static Boolean IsSealedClass(BaseTypeDeclarationSyntax selectedElement) => selectedElement.HasModifier(SyntaxKind.SealedKeyword);

        private static Boolean IsAbstractClass(BaseTypeDeclarationSyntax selectedElement) => selectedElement.HasModifier(SyntaxKind.AbstractKeyword);
    }
}