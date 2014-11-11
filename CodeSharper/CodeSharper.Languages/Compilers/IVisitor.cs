using Antlr4.Runtime;

namespace CodeSharper.Languages.Compilers
{
    public interface IVisitor<out TResult>
    {
        TResult Visit(RuleContext context);
    }
}