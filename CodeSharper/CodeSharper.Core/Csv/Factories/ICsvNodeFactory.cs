using System.Collections.Generic;
using CodeSharper.Core.Csv.Nodes;

namespace CodeSharper.Core.Csv.Factories
{
    public interface ICsvNodeFactory
    {
        FieldNode Field(string value);
        RecordNode Record(IEnumerable<FieldNode> fields);
        CsvCompilationUnit CompilationUnit(RecordNode[] records);
        CommaNode Comma();
    }
}