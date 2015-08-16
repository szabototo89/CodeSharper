using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Languages.CSharp.Nodes.Selectors.ClassSelectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Nodes.Selectors
{
    public class ForeachStatementSelector : TypedSelectorBase<ForEachStatementSyntax>
    {
    }
}