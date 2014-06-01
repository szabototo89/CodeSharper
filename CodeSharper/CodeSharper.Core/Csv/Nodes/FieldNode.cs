using System;
using System.CodeDom;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Csv.Nodes
{
    public class FieldNode : CsvMutableNode
    {
        #region Public properties

        public string Value { get; set; }

        #endregion

        #region Protected methods

        protected override void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor)
        {
            nodeTypeDescriptor.Type = CsvNodeType.Field;
        }

        #endregion
    }
}
