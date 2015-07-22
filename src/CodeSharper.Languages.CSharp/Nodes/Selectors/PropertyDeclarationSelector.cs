using CodeSharper.Languages.CSharp.Nodes.Selectors.ClassSelectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Nodes.Selectors
{
    public class PropertyDeclarationSelector : CSharpTypedSelectorBase<PropertyDeclarationSyntax, IdentifierMatchingClassSelector>
    {
        /// <summary>
        /// Selects the syntax token.
        /// </summary>
        protected override SyntaxToken SelectSyntaxToken(PropertyDeclarationSyntax element)
        {
            return element.Identifier;
        }
    }
}