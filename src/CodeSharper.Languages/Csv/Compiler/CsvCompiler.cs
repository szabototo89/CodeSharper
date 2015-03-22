using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Trees;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Csv.Factories;
using CodeSharper.Languages.Csv.Grammar;
using CodeSharper.Languages.Csv.Visitors;

namespace CodeSharper.Languages.Csv.Compiler
{
    public class CsvCompiler
    {
        public Node Parse<TResult>(String input, ISyntaxTreeVisitor<TResult, IParseTree> treeVisitor, IHasSyntaxTree treeFactory)
        {
            Assume.NotNull(input, "input");
            Assume.NotNull(treeVisitor, "treeVisitor");
            Assume.NotNull(treeFactory, "treeFactory");

            var stream = new AntlrInputStream(input);
            ITokenSource lexer = new CsvLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            var parser = new CsvParser(tokens) {
                BuildParseTree = true
            };

            var tree = parser.start();

            treeVisitor.Visit(input, tree);

            return treeFactory.GetSyntaxTree().Safe(syntaxTree => syntaxTree.Root);
        }

        public Node Parse(String input)
        {
            Assume.NotNull(input, "input");

            var factory = new CsvStandardTreeFactory();
            return Parse(input, new CsvSyntaxTreeBuilder(factory), factory);
        }
    }
}
