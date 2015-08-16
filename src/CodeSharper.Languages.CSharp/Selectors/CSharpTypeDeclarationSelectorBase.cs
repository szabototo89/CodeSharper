using CodeSharper.Languages.CSharp.Selectors.ClassSelectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Selectors
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