using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public static class CsvSyntax
    {
        public static FieldSyntax Field(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            return new FieldSyntax(textRange);
        }

        public static RowSyntax Row(TextRange textRange, IEnumerable<CommaToken> commas, IEnumerable<FieldSyntax> fields)
        {
            Assume.NotNull(commas, "commas");
            Assume.NotNull(fields, "fields");

            return new RowSyntax(textRange) {
                Commas = commas,
                Fields = fields
            };
        }

        public static CsvCompilationUnit CompilationUnit(TextRange textRange, IEnumerable<RowSyntax> rows)
        {
            Assume.NotNull(textRange, "textRange");
            Assume.NotNull(rows, "rows");

            return new CsvCompilationUnit(textRange, rows);
        }
    }
}
