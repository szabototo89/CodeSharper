using System;
using Antlr4.Runtime;
using CodeSharper.Languages.Grammar;

namespace CodeSharper.Languages.Compilers.CodeSharper
{
    public class CodeSharperCompiler
    {
        public TResult RunVisitor<TVisitor, TResult>(String source)
            where TVisitor: IVisitor<TResult>, new()
        {
            var inputStream = new AntlrInputStream(source);
            var lexer = new CodeSharperGrammarLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new CodeSharperGrammarParser(commonTokenStream);
            var context = parser.start();
            var visitor = new TVisitor();
            return visitor.Visit(context);
        }
    }
}
