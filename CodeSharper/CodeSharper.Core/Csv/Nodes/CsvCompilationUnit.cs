using System.Collections.Generic;

namespace CodeSharper.Core.Csv.Nodes
{
    public class CsvCompilationUnit : CsvMutableNode
    {
        public CsvCompilationUnit(IEnumerable<RecordNode> records)
        {
            InitializeChildren(records);
        }

        private void InitializeChildren(IEnumerable<RecordNode> records)
        {
            _Children.AddRange(records);
        }

        protected override void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor)
        {
            nodeTypeDescriptor.Type = NodeType.CompilationUnit;
        }
    }
}