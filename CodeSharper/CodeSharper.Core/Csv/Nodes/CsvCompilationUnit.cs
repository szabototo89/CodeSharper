using System.Collections.Generic;

namespace CodeSharper.Core.Csv.Nodes
{
    public class CsvCompilationUnit : CsvMutableNode
    {
        #region Constructors

        public CsvCompilationUnit(IEnumerable<RecordNode> records)
        {
            InitializeChildren(records);
        }

        #endregion

        #region Private methods

        private void InitializeChildren(IEnumerable<RecordNode> records)
        {
            _Children.AddRange(records);
        }

        #endregion

        #region Protected methods

        protected override void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor)
        {
            nodeTypeDescriptor.Type = NodeType.CompilationUnit;
        }

        #endregion
    }
}