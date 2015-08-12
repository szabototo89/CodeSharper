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
using CodeSharper.Languages.Csv.Grammar;
using CodeSharper.Languages.Json.Factories;
using CodeSharper.Languages.Json.Visitors;

namespace CodeSharper.Languages.Json.Compiler
{
    public class JsonCompiler
    {
        /// <summary>
        /// Parses the specified JSON input.
        /// </summary>
        public Node Parse(String input)
        {
            Assume.NotNull(input, nameof(input));

            var stream = new AntlrInputStream(input);
            ITokenSource lexer = new JsonLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            var parser = new JsonParser(tokens) {
                BuildParseTree = true
            };

            var tree = parser.start();

            var treeFactory = new StandardJsonSyntaxTreeFactory();
            var syntaxBuilder = new StandardJsonSyntaxTreeBuilder(treeFactory);

            return syntaxBuilder.Visit(input, tree);
        }

    }
}
