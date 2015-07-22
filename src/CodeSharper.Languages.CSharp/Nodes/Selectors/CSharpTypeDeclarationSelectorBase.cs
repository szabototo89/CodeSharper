using System.Text.RegularExpressions;
using CodeSharper.Languages.CSharp.Nodes.Selectors.ClassSelectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Nodes.Selectors
{
    public class CSharpTypeDeclarationSelectorBase<TElement> : CSharpTypedSelectorBase<TElement, IdentifierMatchingClassSelector>
        where TElement : BaseTypeDeclarationSyntax
    {
        /// <summary>
        /// Selects the syntax token.
        /// </summary>
        protected override SyntaxToken SelectSyntaxToken(TElement element)
        {
            return element.Identifier;
        }
    }
}