using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Visitors;
using DefaultSelectorFactory = CodeSharper.Interpreter.Visitors.DefaultSelectorFactory;
using ISelectorFactory = CodeSharper.Interpreter.Visitors.ISelectorFactory;

namespace CodeSharper.Interpreter.Compiler
{
    public class CodeQueryCompiler
    {
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        public ControlFlowElementBase Parse(String input)
        {
            return Parse(input,new DefaultSelectorFactory(), new DefaultCodeQueryCommandFactory());
        }

        /// <summary>
        /// Parses the specified input.
        /// </summary>
        public ControlFlowElementBase Parse(String input, ISelectorFactory selectorFactory, ICodeQueryCommandFactory factory)
        {
            Assume.NotNull(input, "input");
            Assume.NotNull(factory, "factory");

            var stream = new AntlrInputStream(input);
            ITokenSource lexer = new CodeQueryLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            var parser = new CodeQuery(tokens) {
                BuildParseTree = true
            };
            var start = parser.command();

            var visitor = new CodeQuerySyntaxTreeBuilder(selectorFactory, factory);
            return visitor.Visit(start) as ControlFlowElementBase;
        }
    }
}
