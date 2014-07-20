using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Csv.Nodes
{
    public class RecordNode : CsvMutableNode
    {
        #region Constructors

        public RecordNode(IEnumerable<FieldNode> fields, IEnumerable<DelimiterNode> delimiters)
        {
            InitializeChildren(fields, delimiters);
        }

        #endregion

        #region Private methods

        private void InitializeChildren(IEnumerable<FieldNode> fields, IEnumerable<DelimiterNode> delimiters)
        {
            _Children.AddRange(fields.Union<MutableNode>(delimiters)
                                     .OrderBy(node => node.GetTextInformation()
                                                          .TextSpan
                                                          .Start));
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

        public IEnumerable<DelimiterNode> GetDelimiters()
        {
            return GetChildren().OfType<DelimiterNode>();
        } 

        #endregion
    }
}