using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Csv.Nodes
{
    public class RecordNode : CsvMutableNode
    {
        public RecordNode(IEnumerable<FieldNode> fields)
        {
            InitializeChildren(fields);
        }

        private void InitializeChildren(IEnumerable<FieldNode> fields)
        {
            _Children.AddRange(fields);
        }

        protected override void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor)
        {
            nodeTypeDescriptor.Type = CsvNodeType.Record;
        }

        public IEnumerable<FieldNode> GetFields()
        {
            return GetChildren().OfType<FieldNode>();
        }
    }
}