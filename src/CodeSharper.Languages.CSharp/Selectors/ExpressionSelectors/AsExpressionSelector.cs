using Microsoft.CodeAnalysis.CSharp;

namespace CodeSharper.Languages.CSharp.Selectors.ExpressionSelectors
{
    public class AsExpressionSelector : TypeCastingLikeExpressionSelector
    {
        public AsExpressionSelector() : base(SyntaxKind.AsExpression)
        {
        }
    }
}