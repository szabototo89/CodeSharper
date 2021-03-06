﻿using System.Linq;
using CodeSharper.Languages.CSharp.Selectors.ClassSelectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Selectors.DeclarationSelectors
{
    public class LocalVariableDeclarationSelector : CSharpTypedSelectorBase<LocalDeclarationStatementSyntax, IdentifierMatchingClassSelector>
    {
        /// <summary>
        /// Selects the syntax token.
        /// </summary>
        protected override SyntaxToken SelectSyntaxToken(LocalDeclarationStatementSyntax element)
        {
            return element.Declaration
                          .Variables
                          .Select(variable => variable.Identifier)
                          .FirstOrDefault();
        }
    }
}