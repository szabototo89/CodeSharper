using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CodeSharper.Languages.CSharp.Selectors.ExpressionSelectors
{
    public class IsExpressionSelector : TypeCastingLikeExpressionSelector
    {
        public IsExpressionSelector() : base(SyntaxKind.IsExpression)
        {
        }
    }
}