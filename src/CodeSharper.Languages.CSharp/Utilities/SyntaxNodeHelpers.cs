using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Utilities
{
    public static class SyntaxNodeHelpers
    {
        public static Boolean HasModifier(this BaseTypeDeclarationSyntax declaration, SyntaxKind modifierSyntaxKind)
        {
            return declaration.Modifiers.Any(modifier => modifier.Kind() == modifierSyntaxKind);
        }
    }
}