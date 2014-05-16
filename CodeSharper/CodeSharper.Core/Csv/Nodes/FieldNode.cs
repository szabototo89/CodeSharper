using System;
using System.CodeDom;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Csv.Nodes
{
    public class FieldNode : CsvMutableNode
    {
        protected override void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor)
        {
            nodeTypeDescriptor.Type = CsvNodeType.Field;
        }

        public string Value { get; set; }
    }
}
