using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Csv.Nodes
{
    public class RecordNode : CsvMutableNode
    {
        #region Constructors

        public RecordNode(IEnumerable<FieldNode> fields)
        {
            InitializeChildren(fields);
        }

        #endregion

        #region Private methods

        private void InitializeChildren(IEnumerable<FieldNode> fields)
        {
            _Children.AddRange(fields);
        }

        #endregion

        #region Protected methods

        protected override void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor)
        {
            nodeTypeDescriptor.Type = CsvNodeType.Record;
        }

        #endregion

        #region Public methods

        public IEnumerable<FieldNode> GetFields()
        {
            return GetChildren().OfType<FieldNode>();
        }

        #endregion
    }
}