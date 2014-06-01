using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Csv.Nodes
{
    public abstract class CsvMutableNode : MutableNode
    {
        #region Protected fields

        protected CsvNodeTypeDescriptor _NodeTypeDescriptor;

        #endregion

        #region Constructors

        protected CsvMutableNode()
        {
            _NodeTypeDescriptor = new CsvNodeTypeDescriptor();
            InitializeNodeTypeDescriptor(_NodeTypeDescriptor);
        }

        #endregion

        #region Protected methods

        protected abstract void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor);

        #endregion

        #region Public methods

        public override NodeTypeDescriptor GetNodeTypeDescriptor()
        {
            return _NodeTypeDescriptor;
        }

        #endregion
    }
}
