using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Csv.Nodes
{
    public abstract class CsvMutableNode : MutableNode
    {
        protected CsvNodeTypeDescriptor _NodeTypeDescriptor;

        protected CsvMutableNode()
        {
            _NodeTypeDescriptor = new CsvNodeTypeDescriptor();
            InitializeNodeTypeDescriptor(_NodeTypeDescriptor);
        }

        protected abstract void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor);

        public override NodeTypeDescriptor GetNodeTypeDescriptor()
        {
            return _NodeTypeDescriptor;
        }
    }
}
