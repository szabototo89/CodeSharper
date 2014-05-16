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
    public class CsvCompiler : CsvBaseVisitor<MutableNode>
    {
        protected CsvTreeFactory Factory;

        public CsvCompiler()
        {
            Factory = new CsvTreeFactory();
        }

        public override MutableNode VisitField(CsvParser.FieldContext context)
        {
            string value = string.Empty;

            if (context.STRING() != null)
                value = context.STRING().GetText();
            else if (context.ID() != null)
                value = context.ID().GetText();

            return Factory.Field(value);
        }
    }
}
