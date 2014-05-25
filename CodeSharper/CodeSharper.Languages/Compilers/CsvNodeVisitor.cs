using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using CodeSharper.Core;
using CodeSharper.Core.Csv;
using CodeSharper.Core.Csv.Factories;
using CodeSharper.Core.Csv.Nodes;
using CodeSharper.Core.Texts;
using CodeSharper.Languages.Grammar;

namespace CodeSharper.Languages.Compilers
{
    public class CsvNodeVisitor : CsvBaseVisitor<CsvMutableNode>
    {
        protected CsvTreeFactory Factory;
        protected CsvAbstractSyntaxTree Ast;

        public CsvNodeVisitor()
        {
            Ast = new CsvAbstractSyntaxTree();
            Factory = new CsvTreeFactory(Ast);
        }

        public CsvAbstractSyntaxTree AbstractSyntaxTree { get { return Ast; } }

        private TextSpan GenerateTextSpanFromContext(ParserRuleContext context)
        {
            var start = new TextLocation(context.Start.Column, context.Start.Line, context.Start.StartIndex);
            var text = context.GetText();
            /*if (context.ChildCount > 1)
            {
                var stop = new TextLocation(context.Stop.Column, context.Stop.Line);
                var span = new TextSpan(start, stop, text);
                return span;
            }*/
            return new TextSpan(start, text);
        }

        public override CsvMutableNode VisitDelimiter(CsvParser.DelimiterContext context)
        {
            return Factory.UpdateTextSpan(GenerateTextSpanFromContext(context))
                          .Comma();
        }

        public override CsvMutableNode VisitField(CsvParser.FieldContext context)
        {
            string value = string.Empty;

            if (context.STRING() != null)
                value = context.STRING().GetText();
            else if (context.ID() != null)
                value = context.ID().GetText();

            return Factory.UpdateTextSpan(GenerateTextSpanFromContext(context))
                          .Field(value);
        }

        public override CsvMutableNode VisitRecord(CsvParser.RecordContext context)
        {
            var fields = context.field()
                .Select(field => field.Accept(this))
                .Cast<FieldNode>();

            return Factory.UpdateTextSpan(GenerateTextSpanFromContext(context))
                          .Record(fields);
        }

        public override CsvMutableNode VisitCompileUnit(CsvParser.CompileUnitContext context)
        {
            var records = context.record()
                                 .Select(record => record.Accept(this))
                                 .Cast<RecordNode>()
                                 .ToArray();

            var node = Factory.UpdateTextSpan(GenerateTextSpanFromContext(context))
                              .CompilationUnit(records);

            SetRootOfAbstractSyntaxTree(node);

            return node;
        }

        private void SetRootOfAbstractSyntaxTree(CsvCompilationUnit node)
        {
            Ast.Root = node;
        }
    }
}
