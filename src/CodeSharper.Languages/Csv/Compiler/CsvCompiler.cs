﻿using System;
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
using CodeSharper.Languages.Csv.SyntaxTrees;
using CodeSharper.Languages.Csv.Visitors;

namespace CodeSharper.Languages.Csv.Compiler
{
    public class CsvCompiler
    {
        public Node Parse<TResult>(String input, ISyntaxTreeVisitor<TResult, IParseTree> treeVisitor, IHasSyntaxTree treeFactory)
        {
            Assume.NotNull(input, nameof(input));
            Assume.NotNull(treeVisitor, nameof(treeVisitor));
            Assume.NotNull(treeFactory, nameof(treeFactory));

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

        public CsvNode Parse(String input, ISyntaxTreeVisitor<CsvNode, IParseTree> treeVisitor)
        {
            Assume.NotNull(input, nameof(input));
            Assume.NotNull(treeVisitor, nameof(treeVisitor));

            var stream = new AntlrInputStream(input);
            ITokenSource lexer = new CsvLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            var parser = new CsvParser(tokens) {
                BuildParseTree = true
            };

            var tree = parser.start();

            return treeVisitor.Visit(input, tree);
        }

        public Node Parse(String input)
        {
            Assume.NotNull(input, nameof(input));

            var factory = new CsvStandardTreeFactory();
            return Parse(input, new CsvSyntaxTreeBuilder(factory), factory);
        }
    }
}
