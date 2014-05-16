using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Csv.Nodes;

namespace CodeSharper.Core.Csv.Factories
{
    public class CsvTreeFactory
    {
        public FieldNode Field(string value)
        {
            if (value == null)
                throw ThrowHelper.ArgumentNullException("value");

            return new FieldNode() { Value = value };
        }

        public RecordNode Record(IEnumerable<FieldNode> fields)
        {
            return new RecordNode(fields);
        }

        public CsvCompilationUnit CompilationUnit(RecordNode[] records)
        {
            return new CsvCompilationUnit(records);
        }
    }
}
