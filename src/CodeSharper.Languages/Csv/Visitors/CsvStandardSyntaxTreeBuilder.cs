using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Languages.Csv.Grammar;
using CodeSharper.Languages.Csv.SyntaxTrees;
using CodeSharper.Languages.Utilities;

namespace CodeSharper.Languages.Csv.Visitors
{
    public class CsvStandardSyntaxTreeBuilder : CsvBaseVisitor<CsvNode>, 
                                                ISyntaxTreeVisitor<CsvNode, IParseTree>
    {
        private TextDocument _textDocument;

        public virtual CsvNode Visit(String input, IParseTree tree)
        {
            Assume.NotNull(input, "input");
            Assume.NotNull(tree, "tree");

            _textDocument = new TextDocument(input);

            return base.Visit(tree);
        }

        public override CsvNode VisitStart(CsvParser.StartContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            var rows = context.row().AcceptAll(this).OfType<RowSyntax>();

            return CsvSyntax.CompilationUnit(textRange, rows);
        }

        public override CsvNode VisitRow(CsvParser.RowContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);

            var fields = context.field().AcceptAll(this).OfType<FieldSyntax>();
            var commas = context.COMMA()
                                .Select(comma => comma.CreateTextRange(_textDocument))
                                .Select(CsvTokens.Comma)
                                .ToArray();

            return CsvSyntax.Row(textRange, commas, fields);
        }

        public override CsvNode VisitField(CsvParser.FieldContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);

            return CsvSyntax.Field(textRange);
        }
    }
}
