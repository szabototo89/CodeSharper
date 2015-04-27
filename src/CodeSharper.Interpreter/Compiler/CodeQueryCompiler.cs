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

namespace CodeSharper.Interpreter.Compiler
{
    public class CodeQueryCompiler
    {
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        public ControlFlowDescriptorBase Parse(String input)
        {
            return Parse(input,new DefaultNodeSelectorFactory(), new DefaultCodeQueryCommandFactory());
        }

        /// <summary>
        /// Parses the specified input.
        /// </summary>
        public ControlFlowDescriptorBase Parse(String input, INodeSelectorFactory nodeSelectorFactory, ICodeQueryCommandFactory factory)
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

            var visitor = new CodeQuerySyntaxTreeBuilder(nodeSelectorFactory, factory);
            return visitor.Visit(start) as ControlFlowDescriptorBase;
        }
    }
}
