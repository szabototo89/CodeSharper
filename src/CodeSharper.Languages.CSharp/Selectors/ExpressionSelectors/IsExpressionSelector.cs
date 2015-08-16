using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Selectors.ExpressionSelectors
{
    public class IsExpressionSelector : ExpressionSelectorBase<BinaryExpressionSyntax>
    {
        public IsExpressionSelector() : base(SyntaxKind.IsExpression)
        {
            
        }
    }

    public class AsExpressionSelector : ExpressionSelectorBase<BinaryExpressionSyntax>
    {
        public AsExpressionSelector() : base(SyntaxKind.AsExpression)
        {

        }
    }

}