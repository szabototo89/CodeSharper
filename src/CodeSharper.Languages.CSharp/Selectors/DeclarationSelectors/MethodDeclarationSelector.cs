using CodeSharper.Languages.CSharp.Selectors.ClassSelectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Selectors.DeclarationSelectors
{
    public class MethodDeclarationSelector : CSharpTypedSelectorBase<MethodDeclarationSyntax, IdentifierMatchingClassSelector>
    {
        /// <summary>
        /// Selects the syntax token.
        /// </summary>
        protected override SyntaxToken SelectSyntaxToken(MethodDeclarationSyntax element)
        {
            return element.Identifier;
        }
    }
}