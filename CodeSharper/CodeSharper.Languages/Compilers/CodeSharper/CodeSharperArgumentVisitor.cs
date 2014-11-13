using System;
using Antlr4.Runtime;
using CodeSharper.Languages.Grammar;

namespace CodeSharper.Languages.Compilers.CodeSharper
{
    public class CodeSharperArgumentVisitor : CodeSharperGrammarBaseVisitor<Object>, IVisitor<Object>
    {
        public Object Visit(RuleContext context)
        {
            return VisitParameter(context as CodeSharperGrammarParser.ParameterContext);
        }
    }
}