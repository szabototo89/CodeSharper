using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using CodeSharper.Core;
using CodeSharper.Core.Csv;
using CodeSharper.Core.Csv.Nodes;
using CodeSharper.Languages.Grammar;

namespace CodeSharper.Languages.Compilers
{
    public class CsvCompiler
    {
        public static CsvAbstractSyntaxTree CompileFromString(string code)
        {
            return new CsvCompiler().Compile(code);
        }

        public CsvAbstractSyntaxTree Compile(string code)
        {
            var reader = new StringReader(code);
            var input = new AntlrInputStream(reader);
            var lexer = new CsvLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CsvParser(tokens);

            var visitor = new CsvNodeVisitor();
            visitor.Visit(parser.compileUnit());
            return visitor.AbstractSyntaxTree;
        }
    }
}