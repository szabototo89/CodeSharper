using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core;
using CodeSharper.Core.Csv.Factories;
using CodeSharper.Core.Csv.Nodes;
using CodeSharper.Languages.Grammar;

namespace CodeSharper.Languages.Compilers
{
    public class CsvCompiler : CsvBaseVisitor<CsvMutableNode>
    {
        protected CsvTreeFactory Factory;

        public CsvCompiler()
        {
            Factory = new CsvTreeFactory();
        }

        public override CsvMutableNode VisitDelimiter(CsvParser.DelimiterContext context)
        {
            return Factory.Comma();
        }

        public override CsvMutableNode VisitField(CsvParser.FieldContext context)
        {
            string value = string.Empty;

            if (context.STRING() != null)
                value = context.STRING().GetText();
            else if (context.ID() != null)
                value = context.ID().GetText();

            return Factory.Field(value);
        }

        public override CsvMutableNode VisitRecord(CsvParser.RecordContext context)
        {
            var fields = context.field()
                .Select(field => field.Accept(this))
                .Cast<FieldNode>();

            return Factory.Record(fields);
        }

        public override CsvMutableNode VisitCompileUnit(CsvParser.CompileUnitContext context)
        {
            var records = context.record()
                                 .Select(record => record.Accept(this))
                                 .Cast<RecordNode>()
                                 .ToArray();

            return Factory.CompilationUnit(records);
        }
    }
}
