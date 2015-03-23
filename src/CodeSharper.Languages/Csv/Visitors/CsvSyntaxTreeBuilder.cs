using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Csv.Factories;
using CodeSharper.Languages.Csv.Grammar;
using CodeSharper.Languages.Csv.SyntaxTrees;
using CodeSharper.Languages.Utilities;

namespace CodeSharper.Languages.Csv.Visitors
{
    public class CsvSyntaxTreeBuilder : CsvBaseVisitor<CsvSyntaxTreeBuilder>, 
                                        ISyntaxTreeVisitor<CsvSyntaxTreeBuilder, IParseTree>
    {
        private readonly ICsvTreeFactory _treeFactory;
        private TextDocument _textDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvSyntaxTreeBuilder"/> class.
        /// </summary>
        public CsvSyntaxTreeBuilder(ICsvTreeFactory treeFactory)
        {
            Assume.NotNull(treeFactory, "treeFactory");
            _treeFactory = treeFactory;
        }

        public virtual CsvSyntaxTreeBuilder Visit(String input, IParseTree tree)
        {
            Assume.NotNull(input, "input");
            Assume.NotNull(tree, "tree");

            _textDocument = new TextDocument(input);

            return base.Visit(tree);
        }

        public override CsvSyntaxTreeBuilder VisitStart(CsvParser.StartContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            _treeFactory.CreateDocument(textRange);

            foreach (var row in context.row())
            {
                row.Accept(this);
            }

            return this;
        }

        public override CsvSyntaxTreeBuilder VisitRow(CsvParser.RowContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);

            _treeFactory.CreateRow(textRange);

            foreach (var field in context.field())
                field.Accept(this);

            foreach (var comma in context.COMMA())
            {
                var range = comma.CreateTextRange(_textDocument);
                _treeFactory.CreateComma(range);
            }

            return this;
        }

        public override CsvSyntaxTreeBuilder VisitField(CsvParser.FieldContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            _treeFactory.CreateField(textRange);
            return this;
        }

    }
}
